using System;
using System.Collections;
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
    
    [Header("Jump")]
    [SerializeField]
    private float _jumpForce = 5f;
    
    private CharacterController _characterController;
    
    private float _yVelocity = 0f;
    private bool _jumpAble = false;
    private int _jumpCounter = 0;

    [Header("Dash")]
    [SerializeField]
    private float _dashTime = 0.5f;
    [SerializeField]
    private float _dashForce = 10f;
    private bool _isDash = false;
    
    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 direction = Move();
        Dash();
        // 캐릭터 컨트롤러로 이동
        if (!_isDash)
        {
            Jump();
            direction.y = _yVelocity;
            _characterController.Move(direction * (Player.MoveSpeed * Time.deltaTime));
        }
        else
        {  
            // 대쉬는 중력을 무시
            direction = direction ==  Vector3.zero ? transform.forward : direction; // 이동중이면 이동 방향으로 대쉬 아니면 앞으로
            direction.y = 0f;
            _characterController.Move(direction * (_dashForce * Time.deltaTime));
        }
    }

    private Vector3 Move()
    {
        // 키보드 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = direction.normalized;
        
        // 플레이어 기준으로 방향을 변환
        direction = _camera.transform.TransformDirection(direction);
        
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
    
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        _isDash = true;
        
        yield return new WaitForSeconds(_dashTime);

        _isDash = false;
    }
}
