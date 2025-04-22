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
    // TODO: 중복 입력 예외처리
    private const float GRAVITY = -9.8f;
    private const int MAX_JUMPCOUNT = 2;

    private CharacterController _characterController;
    private Vector3 _direction;

    [Header("Jump")]
    [SerializeField]
    private float _jumpForce = 5f;

    private float _yVelocity = 0f;
    private bool _jumpAble = false;
    private int _jumpCounter = 0;

    [Header("Climbing")]
    [SerializeField]
    private float _climbStaminaPerSecond = 10f;

    [SerializeField]
    private float _climbForce = 11f;

    [SerializeField]
    private Transform _wallRaycastPosition;
    
    private bool _isClimbing = false;

    [Header("Dash")]
    [SerializeField]
    private float _dashForce = 10f;

    private Camera _camera;
    private LayerMask _wallMask;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        _characterController.detectCollisions = true;
        _wallMask = LayerMask.NameToLayer("Wall");
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // 캐릭터 컨트롤러로 이동
        if (!Player.IsDash)
        {
            Move();
            Jump();
            Fall();
            Climb();
            _direction.y = _yVelocity;
            _characterController.Move(_direction * (Player.MoveSpeed * Time.deltaTime));
        }
        else
        {
            // 대쉬는 중력을 무시
            _direction = _direction == Vector3.zero ? transform.forward : _direction; // 이동중이면 이동 방향으로 대쉬 아니면 앞으로
            _direction.y = 0f;
            _characterController.Move(_direction * (_dashForce * Time.deltaTime));
        }
    }

    private void Move()
    {
        // 키보드 입력
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _direction = new Vector3(horizontal, 0, vertical);
        _direction = _direction.normalized;

        // 플레이어 기준으로 방향을 변환
        _direction = _camera.transform.TransformDirection(_direction);
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
    }

    private void Fall()
    {
        if (_characterController.isGrounded)
        {
            return;
        }

        // 중력 적용
        _yVelocity += GRAVITY * Time.deltaTime;
    }

    private void Climb()
    {
        if (_direction == Vector3.zero)
        {
            if (_isClimbing && _characterController.isGrounded)
            {
                // 클라이밍 중 벽과 떨어진 경우 중 움직임이 없는 경우
                EndClimb();
            }
            return;
        }
        // wall만 체크
        Vector3 RayDirection = _direction;
        RayDirection.y = 0f;
        RayDirection.Normalize();
        if (Physics.Raycast(_wallRaycastPosition.position, RayDirection, 2f, 1 << _wallMask))
        {
            float climbStamina = _climbStaminaPerSecond * Time.deltaTime;
            if (Player.TryUseStamina(climbStamina))
            {
                Debug.Log("up");
                _isClimbing = true;
                _yVelocity = _climbForce;
            }
        }
        else if (_isClimbing && _characterController.isGrounded)
        {
            // 클라이밍 중 벽과 떨어진 경우
            EndClimb();
        }
        
        void EndClimb()
        {
            _isClimbing = false;
            Player.IsRecoverStamina = true;
        }
    }
}