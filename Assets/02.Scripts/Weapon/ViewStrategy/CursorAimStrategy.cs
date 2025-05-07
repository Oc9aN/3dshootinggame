using UnityEngine;

public class CursorAimStrategy : IWeaponAimStrategy
{
    public Vector3 WeaponAiming(Weapon weapon)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 aimRayPosition = ray.GetPoint(100f);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            aimRayPosition = hit.point;
        }

        aimRayPosition.y = weapon.transform.position.y;
        Vector3 aimDirection = aimRayPosition - weapon.AttackPosition.position;
        return aimDirection.normalized;
    }
}