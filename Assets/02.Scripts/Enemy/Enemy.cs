using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Trace,
        Return,
        Attack,
        Damaged,
        Die,
    }

    private EnemyState _currentState = EnemyState.Idle;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private int _health;

    [SerializeField]
    private float _findDistance = 7f; // 추격 거리

    [SerializeField]
    private float _attackDistance = 2.5f; // 공격 사거리

    [SerializeField]
    private float _returnDistance = 0.1f; // 복귀 사거리

    [SerializeField]
    private float _moveSpeed = 3.3f; // 이동 속도

    [SerializeField]
    private float _attackCoolTime = 2f; // 공격 쿨타임

    [SerializeField]
    private float _stiffTime = 0.5f; // 경직 시간

    private float _attackCoolTimer = 0f;

    private float _stiffTimer = 0f;

    private CharacterController _characterController;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
            {
                Idle();
                break;
            }
            case EnemyState.Trace:
            {
                Trace();
                break;
            }
            case EnemyState.Return:
            {
                Return();
                break;
            }
            case EnemyState.Attack:
            {
                Attack();
                break;
            }
            case EnemyState.Damaged:
            {
                Damaged();
                break;
            }
            case EnemyState.Die:
            {
                Die();
                break;
            }
        }
    }

    public void TakeDamage(Damage damage)
    {
        _health -= damage.Value;

        Debug.Log($"상태전환 {_currentState}->Damaged");
        _currentState = EnemyState.Damaged;
    }

    // FSM
    private void Idle()
    {
        // 대기 상태
        // 전이 Trace
        if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
        {
            Debug.Log("상태전환 Idle->Trace");
            _currentState = EnemyState.Trace;
            return;
        }
    }

    private void Trace()
    {
        // 전이 Return
        if (Vector3.Distance(transform.position, _player.transform.position) >= _findDistance)
        {
            Debug.Log("상태전환 Trace->Return");
            _currentState = EnemyState.Return;
            return;
        }

        // 전이 Attack
        if (Vector3.Distance(transform.position, _player.transform.position) < _attackDistance)
        {
            Debug.Log("상태전환 Trace->Attack");
            _currentState = EnemyState.Attack;
            return;
        }

        // 플레이어 추적
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
    }

    private void Return()
    {
        // 전이 Idle
        if (Vector3.Distance(transform.position, _startPosition) < _returnDistance)
        {
            Debug.Log("상태전환 Return->Idle");
            _currentState = EnemyState.Idle;
            return;
        }

        // 전이 Trace
        if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
        {
            Debug.Log("상태전환 Return->Trace");
            _currentState = EnemyState.Trace;
            return;
        }

        // 제자리로 복귀
        Vector3 direction = (_startPosition - transform.position).normalized;
        _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
    }

    private void Attack()
    {
        // 전이 Trace
        if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
        {
            Debug.Log("상태전환 Attack->Trace");
            _attackCoolTimer = 0f;
            _currentState = EnemyState.Trace;
            return;
        }

        // 공격
        _attackCoolTimer += Time.deltaTime;
        if (_attackCoolTimer >= _attackCoolTime)
        {
            Debug.Log("Attack");
            _attackCoolTimer = 0f;
        }
    }

    private void Damaged()
    {
        // 피격 = 경직 후 Trace
        _stiffTimer += Time.deltaTime;
        if (_stiffTimer >= _stiffTime)
        {
            _stiffTimer = 0f;
            Debug.Log("상태전환 Damaged->Trace");
            _currentState = EnemyState.Trace;
        }
    }

    private void Die()
    {
        // 사망
    }
}