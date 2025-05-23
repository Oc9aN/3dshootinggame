using UnityEngine;

public class CrosshairRotationStrategy : IRotationStrategy
{
    public float RotationAngle(Player player)
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = InputHandler.GetAxis("Mouse X");
        
        // 2. 마우스 입력으로부터 회전 시킬 크기를 누적한다.
        float yRotation = player.transform.eulerAngles.y;
        yRotation += mouseX * player.Data.RotateSpeed * Time.deltaTime;

        return yRotation;
    }
}