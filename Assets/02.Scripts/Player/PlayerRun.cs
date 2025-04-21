using System;
using UnityEngine;

public class PlayerRun : PlayerComponent
{
    [SerializeField]
    private float _runMoveSpeed = 12f;
    [SerializeField]
    private float _useStaminaPerSecond = 10f;
    
    // 목표: Shift를 누르면 달리기 (스테미나 소모)
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float useStamina = _useStaminaPerSecond * Time.deltaTime;
            if (Player.TryUseStamina(useStamina))
            {
                // 이동속도 변경
                Player.SetMoveSpeed(_runMoveSpeed);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Player.SetDefaultMoveSpeed();
            Player.IsUsingStamina = false;
        }
    }
}
