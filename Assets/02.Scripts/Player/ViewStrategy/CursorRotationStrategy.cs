using UnityEngine;

public class CursorRotationStrategy : IRotationStrategy
{
    public float RotationAngle(Player player)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float targetY = player.transform.position.y;
        Vector3 targetPoint = ray.GetPoint((targetY - ray.origin.y) / ray.direction.y);
        return Quaternion.LookRotation(targetPoint - player.transform.position).eulerAngles.y;
    }
}