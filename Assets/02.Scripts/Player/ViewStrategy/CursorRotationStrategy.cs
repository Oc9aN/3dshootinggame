using UnityEngine;

public class CursorRotationStrategy : IRotationStrategy
{
    public float RotationAngle(Player player)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 lookTarget = ray.GetPoint(100f);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            lookTarget = hit.point;
        }
        lookTarget.y = player.transform.position.y;
        return Quaternion.LookRotation(lookTarget - player.transform.position).eulerAngles.y;
    }
}