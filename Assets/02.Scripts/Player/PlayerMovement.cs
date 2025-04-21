using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerMovement : PlayerComponent
{
    // 목표: WASD로 캐릭터를 카메라에 맞게 이동
    
    // 구현 순서
    // 1. 키보드 입력을 받는다.
    // 2. 입력으로부터 방향을 설정한다.
    // 3. 방향에 따라 플레이어를 이동한다.
    // TODO: 달리면서 점프하면 더 높게 뛰는 것 수정
    private const float GRAVITY = -9.8f;
    private const int MAX_JUMPCOUNT = 2;
    
    [SerializeField]
    private float _jumpForce = 5f;
    
    private CharacterController _characterController;
    
    private float _yVelocity = 0f;
    private bool _jumpAble = false;
    private int _jumpCounter = 0;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = Move();
        Jump();
        direction.y = _yVelocity;
        // 캐릭터 컨트롤러로 이동
        _characterController.Move(direction * (Player.MoveSpeed * Time.deltaTime));
    }

    private Vector3 Move()
    {
        // 키보드 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;
        
        // 플레이어 기준으로 방향을 변환
        direction = Camera.main.transform.TransformDirection(direction);
        
        return direction;
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
            _yVelocity = _jumpForce;
            _jumpCounter++;
            if (_jumpCounter >= MAX_JUMPCOUNT)
            {
                // 더 많이 뛰면 점프 막기
                _jumpAble = false;
            }
        }
        
        // 중력 적용
        _yVelocity += GRAVITY * Time.deltaTime;
    }
}
