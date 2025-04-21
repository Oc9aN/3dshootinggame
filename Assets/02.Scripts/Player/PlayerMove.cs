using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    // 목표: WASD로 캐릭터를 카메라에 맞게 이동
    
    // 구현 순서
    // 1. 키보드 입력을 받는다.
    // 2. 입력으로부터 방향을 설정한다.
    // 3. 방향에 따라 플레이어를 이동한다.
    private const float GRAVITY = -9.8f;
    
    [SerializeField]
    private float _jumpForce = 10f;
    
    private CharacterController _characterController;
    
    private float _yVelocity = 0f;
    private bool _isJumping = false;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 키보드 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;
        
        // 플레이어 기준으로 방향을 변환
        direction = Camera.main.transform.TransformDirection(direction);

        if (_characterController.isGrounded)
        {
            _isJumping = false;
        }
        
        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
        {
            _yVelocity = _jumpForce;

            _isJumping = true;
        }
        
        // 중력 적용
        _yVelocity += GRAVITY * Time.deltaTime;
        direction.y = _yVelocity;

        // 캐릭터 컨트롤러로 이동
        _characterController.Move(direction * (Player.MoveSpeed * Time.deltaTime));
    }
}
