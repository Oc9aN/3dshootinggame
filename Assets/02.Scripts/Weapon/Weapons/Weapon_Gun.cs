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
            
            // TODO: 시야에 따라 다르게
            // 카메라 크로스헤어 기준으로 레이를 쏜 경우
            Vector3 aimDirection = _camera.transform.forward;
            Ray cameraRay = new Ray(_camera.transform.position, aimDirection);

            Vector3 cameraHitPoint = _camera.transform.position + aimDirection * _data.BulletMaxDistance; // 안맞으면 최대 거리까지 존재
            Debug.DrawRay(_camera.transform.position, aimDirection * _data.BulletMaxDistance, Color.red, 5f);
            if (Physics.Raycast(cameraRay, out RaycastHit cameraHit, _data.BulletMaxDistance,
                    ~(1 << LayerMask.NameToLayer("Player"))))
            {
                cameraHitPoint = cameraHit.point;
            }

            // 카메라로 조준한 위치를 총구에서부터 새로운 레이로 체크 => 실제 총알에 맞는 것처럼 하기 위함.
            Vector3 hitDirection = cameraHitPoint - _attackPosition.position;
            Vector3 hitNormal = -aimDirection.normalized;
            Vector3 gunHitPoint = _attackPosition.position + hitDirection * _data.BulletMaxDistance; // 안맞으면 최대 거리까지 존재
            
            Debug.DrawRay(_attackPosition.position, hitDirection * _data.BulletMaxDistance, Color.yellow, 5f);
            Ray gunRay = new Ray(_attackPosition.position, hitDirection);
            if (Physics.Raycast(gunRay, out RaycastHit gunHit, _data.BulletMaxDistance,
                    ~(1 << LayerMask.NameToLayer("Player"))))
            {
                hitNormal = gunHit.normal;
                gunHitPoint = gunHit.point;
                if (gunHit.collider.TryGetComponent(out IDamageable damageableObject))
                {
                    _data.Damage.From = gameObject;

                    damageableObject.TakeDamage(_data.Damage);
                }
            }

            // 허공에 쏘는 것도 쏘는 것
            Bullet bullet = Pool_Bullet.Instance.GetPooledObject();
            bullet.transform.position = _attackPosition.position;
            bullet.Fire(gunHitPoint, _data.BulletSpeed, hitNormal);

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