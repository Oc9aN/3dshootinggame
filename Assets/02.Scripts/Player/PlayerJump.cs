using System;
using UnityEngine;

public class PlayerJump : PlayerComponent
{
    private const int MAX_JUMPCOUNT = 2;
    
    private CharacterController _characterController;

    private bool _jumpAble = false;
    private int _jumpCounter = 0;
    
    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            _jumpAble = true;
            _jumpCounter = 0;
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && _jumpAble)
        {
            Player.YVelocity = Player.Data.JumpForce;
            _jumpCounter++;
            if (_jumpCounter >= MAX_JUMPCOUNT)
            {
                // 더 많이 뛰면 점프 막기
                _jumpAble = false;
            }
        }
    }
}
