using System;
using UnityEngine;

public class PlayerRun : PlayerComponent
{
    // 목표: Shift를 누르면 달리기 (스테미나 소모)
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float useStamina = Player.Data.UseStaminaPerSecond * Time.deltaTime;
            if (Player.TryUseStamina(useStamina))
            {
                // 이동속도 변경
                Player.MoveSpeed = Player.Data.RunMoveSpeed;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Player.MoveSpeed = Player.Data.DefaultMoveSpeed;
            Player.IsRecoverStamina = true;
        }
    }
}
