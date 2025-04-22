using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : PlayerComponent
{
    private CharacterController _characterController;

    private bool _isDashing = false;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.TryUseStamina(Player.Data.DashStaminaCost))
        {
            StartCoroutine(DashCoroutine());
        }
        // 캐릭터 컨트롤러로 이동
        if (_isDashing)
        {
            // 대쉬는 중력을 무시
            Vector3 dashDirection = Player.Direction == Vector3.zero ? transform.forward : Player.Direction; // 이동중이면 이동 방향으로 대쉬 아니면 앞으로
            dashDirection.y = 0f;
            Player.Direction = dashDirection;
            _characterController.Move(Player.Direction * (Player.Data.DashForce * Time.deltaTime));
        }
    }

    private IEnumerator DashCoroutine()
    {
        _isDashing = true;
        Player.IsMoveable = false;

        yield return new WaitForSeconds(Player.Data.DashTime);

        _isDashing = false;
        Player.IsMoveable = true;
        Player.IsRecoverStamina = true;
    }
}
