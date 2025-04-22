using System;
using UnityEngine;

public class PlayerGravity : PlayerComponent
{
    private CharacterController _characterController;
    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        if (_characterController.isGrounded || !Player.ApplyGravity)
        {
            return;
        }

        // 중력 적용
        Player.YVelocity += Player.Data.Gravity * Time.deltaTime;
    }
}
