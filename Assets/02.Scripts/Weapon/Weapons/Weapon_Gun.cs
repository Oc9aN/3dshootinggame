using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon_Gun : Weapon
{
    [SerializeField]
    private ParticleSystem _muzzleFlash;

    private Vector3 _bulletHitPoint;
    private Vector3 _bulletHitNormal;

    private Vector3 _currentRecoil;
    private Vector3 _targetRecoil;

    private IEnumerator _fireCoroutine;

    public override void Attack()
    {
        // 공격방식
        if (_fireRate <= 0 && CurrentAmmo > 0)
        {
            ApplyRandomRecoil(); // 먼저 반동 값을 계산

            Fire();

            if (!ReferenceEquals(_fireCoroutine, null))
            {
                StopCoroutine(_fireCoroutine);
            }
            _fireCoroutine = FireBullet_Coroutine();
            StartCoroutine(_fireCoroutine);

            // 연출
            TriggerAnimation();

            _fireRate = _data.FireRate;
            CurrentAmmo--;
            // 재장전 중이면 중지
            if (IsReloading)
            {
                IsReloading = false;
            }
        }
        // 반동 계산 (발사하지 않을 때도 목표 반동 감소)
        else
        {
            _currentRecoil = Vector3.Lerp(_currentRecoil, _targetRecoil, Time.deltaTime * _data.RecoilSpeed);
            _targetRecoil = Vector3.Lerp(_targetRecoil, Vector3.zero, Time.deltaTime * _data.RecoilReturnSpeed);
        }
    }

    private void LateUpdate()
    {
        _camera.transform.localEulerAngles += _currentRecoil; // 카메라 회전 적용 (LateUpdate로 이동)
    }

    private void Fire()
    {
        // 반동 계산 (LateUpdate에서 적용하므로 여기서는 목표 반동만 설정)
        _currentRecoil = Vector3.Lerp(_currentRecoil, _targetRecoil, Time.deltaTime * _data.RecoilSpeed);
        _targetRecoil = Vector3.Lerp(_targetRecoil, Vector3.zero, Time.deltaTime * _data.RecoilReturnSpeed);

        // 시야에 따라 다르게
        Vector3 hitDirection = _weaponAimStrategy.GetWeaponAimingDirection(this);
        _bulletHitNormal = -hitDirection.normalized;
        _bulletHitPoint = _attackPosition.position + hitDirection * _data.BulletMaxDistance; // 안맞으면 최대 거리까지 존재

        Debug.DrawRay(_attackPosition.position, hitDirection * _data.BulletMaxDistance, Color.yellow, 5f);
        Ray gunRay = new Ray(_attackPosition.position, hitDirection);
        if (Physics.Raycast(gunRay, out RaycastHit gunHit, _data.BulletMaxDistance,
                ~(1 << LayerMask.NameToLayer("Player"))))
        {
            _bulletHitNormal = gunHit.normal;
            _bulletHitPoint = gunHit.point;
            if (gunHit.collider.TryGetComponent(out IDamageable damageableObject))
            {
                _data.Damage.From = gameObject;

                damageableObject.TakeDamage(_data.Damage);
            }
        }
    }

    private IEnumerator FireBullet_Coroutine()
    {
        yield return new WaitForSeconds(_fireRate);
        _muzzleFlash.Play();
        TriggerOnAttack();
        Bullet bullet = Pool_Bullet.Instance.GetPooledObject();
        bullet.transform.position = _attackPosition.position;
        bullet.Fire(_bulletHitPoint, _data.BulletSpeed, _bulletHitNormal);
    }

    private void ApplyRandomRecoil()
    {
        float vertical = Random.Range(0f, _data.VerticalRecoil);                          // 위로 튕김
        float horizontal = Random.Range(-_data.HorizontalRecoil, _data.HorizontalRecoil); // 좌우 랜덤
        _targetRecoil += new Vector3(vertical, horizontal, 0f);
    }
}