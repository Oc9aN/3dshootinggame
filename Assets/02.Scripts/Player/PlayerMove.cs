using System;
using System.Collections;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    // 목표: WASD로 캐릭터를 카메라에 맞게 이동

    // 구현 순서
    // 1. 키보드 입력을 받는다.
    // 2. 입력으로부터 방향을 설정한다.
    // 3. 방향에 따라 플레이어를 이동한다.
    // TODO: 달리면서 점프하면 더 높게 뛰는 것 수정
    // TODO: 중복 입력 예외처리
    private const float GRAVITY = -9.8f;

    private CharacterController _characterController;

    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        _characterController.detectCollisions = true;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!Player.IsMoveable)
        {
            return;
        }
        // 키보드 입력
        float horizontal = InputHandler.GetAxis("Horizontal");
        float vertical = InputHandler.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Player.Animator.SetFloat("Movement", direction.magnitude);
        
        direction.Normalize();

        // 플레이어 기준으로 방향을 변환
        direction = _camera.transform.TransformDirection(direction);
        
        // Y가속도 적용
        direction.y = Player.YVelocity;
        Player.Direction = direction;
        _characterController.Move(Player.Direction * (Player.MoveSpeed * Time.deltaTime));
    }
}