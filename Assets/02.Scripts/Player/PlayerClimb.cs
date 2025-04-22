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
        // 클라이밍 중이 아니고 방향이 Zero면 계산 안함
        if (!_isClimbing && Player.Direction == Vector3.zero)
        {
            return;
        }

        // wall만 체크
        // 여러 방향에서 벽을 타기 위해 Raycast활용
        // 벽 방향으로 이동하면 벽타기
        Vector3 rayDirection = Player.Direction;
        rayDirection.y = 0f;
        rayDirection.Normalize();
        float climbStamina = Player.Data.ClimbStaminaPerSecond * Time.deltaTime;
        if (Physics.Raycast(_wallRaycastPosition.position, rayDirection, 2f, 1 << _wallMask) &&
            Player.TryUseStamina(climbStamina))
        {
            if (!_isClimbing)
            {
                _isClimbing = true;
                Player.ApplyGravity = false;
                Player.YVelocity = Player.Data.ClimbForce;
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