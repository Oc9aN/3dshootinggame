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

    private Vector3 _direction;
    
    private float _yVelocity = 0f;
    private bool _jumpAble = false;
    private int _jumpCounter = 0;

    private GameObject _wall;
    private bool _isClimbing = false;

    [Header("Dash")]
    [SerializeField]
    private float _dashTime = 0.5f;
    [SerializeField]
    private float _dashForce = 10f;
    [SerializeField]
    private float _dashStamina = 20f;
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
        Move();
        Dash();
        Climb();
        // 캐릭터 컨트롤러로 이동
        if (!_isDash)
        {
            Jump();
            _direction.y = _yVelocity;
            _characterController.Move(_direction * (Player.MoveSpeed * Time.deltaTime));
        }
        else
        {  
            // 대쉬는 중력을 무시
            _direction = _direction ==  Vector3.zero ? transform.forward : _direction; // 이동중이면 이동 방향으로 대쉬 아니면 앞으로
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

        if (_isClimbing)
        {
            return;
        }
        // 중력 적용
        _yVelocity += GRAVITY * Time.deltaTime;
    }
    
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.TryUseStamina(_dashStamina))
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        _isDash = true;
        
        yield return new WaitForSeconds(_dashTime);

        _isDash = false;
        Player.IsUsingStamina = false;
    }

    private void Climb()
    {
        if (ReferenceEquals(_wall, null))
        {
            _isClimbing = false;
            return;
        }
        Vector3 wallDirection = _wall.transform.position - transform.position;
        wallDirection.Normalize();

        float dotResult = Vector3.Dot(wallDirection, transform.forward);
        if (dotResult > 0.7f)
        {
            // 벽과 방향이 얼추 일치
            _yVelocity = 1f;
            _isClimbing = true;
        }

        // 벽에서 벗어난 것 체크를 위해
        _wall = null;
    }

    // TODO: 나가는 함수는 왜 없지..
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Wall"))
        {
            _wall = hit.gameObject;
        }
    }
}
