using UnityEngine;

public class CrosshairAimStrategy : IWeaponAimStrategy
{
    public Vector3 GetWeaponAimingDirection(Weapon weapon)
    {
        // 카메라 크로스헤어 기준으로 레이를 쏜 경우
        Vector3 aimDirection = Camera.main.transform.forward;
        Ray cameraRay = new Ray(Camera.main.transform.position, aimDirection);

        Vector3 cameraHitPoint = Camera.main.transform.position + aimDirection * weapon.Data.BulletMaxDistance; // 안맞으면 최대 거리까지 존재
        Debug.DrawRay(Camera.main.transform.position, aimDirection * weapon.Data.BulletMaxDistance, Color.red, 5f);
        if (Physics.Raycast(cameraRay, out RaycastHit cameraHit, weapon.Data.BulletMaxDistance,
                ~(1 << LayerMask.NameToLayer("Player"))))
        {
            cameraHitPoint = cameraHit.point;
        }
        // 카메라로 조준한 위치를 총구에서부터 새로운 레이로 체크 => 실제 총알에 맞는 것처럼 총알 경로를 생성 하기 위함.
        return (cameraHitPoint - weapon.AttackPosition.position).normalized;
    }
}