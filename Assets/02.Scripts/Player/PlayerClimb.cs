using System;
using UnityEngine;

public class PlayerClimb : PlayerComponent
{
    [SerializeField]
    private Transform _wallRaycastPosition;
    
    private CharacterController _characterController;
    
    private LayerMask _wallMask;
    private bool _isClimbing = false;
    
    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
        _wallMask = LayerMask.NameToLayer("Wall");
    }

    private void Update()
    {
        Climb();
    }

    private void Climb()
    {
        if (Player.Direction == Vector3.zero)
        {
            if (_isClimbing && _characterController.isGrounded)
            {
                // 클라이밍 중 벽과 떨어진 경우 중 움직임이 없는 경우
                EndClimb();
            }
            return;
        }
        // wall만 체크
        Vector3 rayDirection = Player.Direction;
        rayDirection.y = 0f;
        rayDirection.Normalize();
        if (Physics.Raycast(_wallRaycastPosition.position, rayDirection, 2f, 1 << _wallMask))
        {
            float climbStamina = Player.Data.ClimbStaminaPerSecond * Time.deltaTime;
            if (Player.TryUseStamina(climbStamina))
            {
                Debug.Log("up");
                _isClimbing = true;
                Player.YVelocity = Player.Data.ClimbForce;
                Player.ApplyGravity = false;
            }
        }
        else if (_isClimbing)
        {
            // 클라이밍 중 벽과 떨어진 경우
            if (_characterController.isGrounded)
            {
                EndClimb();
            }
            Player.ApplyGravity = true;
        }
        
        void EndClimb()
        {
            _isClimbing = false;
            Player.IsRecoverStamina = true;
            Player.ApplyGravity = true;
        }
    }
}
