using UnityEngine;

public class Weapon_Gun : Weapon
{
    public override void Attack()
    {
        // 공격방식
        if (_fireRate <= 0 && CurrentAmmo > 0)
        {
            ApplyRandomRecoil(); // 먼저 반동 값을 계산

            // 반동 계산
            _currentRecoil = Vector3.Lerp(_currentRecoil, _targetRecoil, Time.deltaTime * _data.RecoilSpeed);
            _targetRecoil = Vector3.Lerp(_targetRecoil, Vector3.zero, Time.deltaTime * _data.RecoilReturnSpeed);

            _camera.transform.localEulerAngles += _currentRecoil; // 카메라 회전 적용

            Vector3 fireDirection = _camera.transform.forward;
            Ray ray = new Ray(_attackPosition.position, fireDirection);
            Vector3 hitPoint = fireDirection * _data.BulletMaxDistance; // 안맞으면 최대 거리까지 존재
            Vector3 hitNormal = fireDirection.normalized;
            if (Physics.Raycast(ray, out RaycastHit hit, ~LayerMask.NameToLayer("Player")))
            {
                hitPoint = hit.point;
                hitNormal = hit.normal;

                if (hit.collider.TryGetComponent(out IDamageable damageableObject))
                {
                    _data.Damage.From = gameObject;

                    damageableObject.TakeDamage(_data.Damage);
                }
            }

            // 허공에 쏘는 것도 쏘는 것
            Bullet bullet = Pool_Bullet.Instance.GetPooledObject();
            bullet.transform.position = _attackPosition.position;
            bullet.Fire(hitPoint, _data.BulletSpeed, hitNormal);

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
            _camera.transform.localEulerAngles += _currentRecoil;
        }
    }
}