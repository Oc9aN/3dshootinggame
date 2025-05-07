using UnityEngine;

public class CursorAimStrategy : IWeaponAimStrategy
{
    public Vector3 WeaponAiming(Weapon weapon)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float targetY = weapon.transform.position.y;
        Vector3 targetPoint = ray.GetPoint((targetY - ray.origin.y) / ray.direction.y);
        Vector3 aimDirection = targetPoint - weapon.AttackPosition.position;
        return aimDirection.normalized;
    }
}