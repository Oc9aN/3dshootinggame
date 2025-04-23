using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Patrol,
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
    private float _onPlaceThreshold = 0.1f; // 제자리 임계점

    [SerializeField]
    private float _moveSpeed = 3.3f; // 이동 속도

    [SerializeField]
    private float _attackCoolTime = 2f; // 공격 쿨타임

    [SerializeField]
    private float _stiffTime = 0.5f; // 경직 시간

    [SerializeField]
    private float _dieTime = 2f; // 사망 시간
    
    [SerializeField]
    private float _patrolTime = 1f;
    
    [SerializeField]
    private List<Transform> _patrolPoints;

    private float _attackCoolTimer = 0f;
    
    private float _patrolTimer = 0f;

    private CharacterController _characterController;

    private Vector3 _targetPosition;    // 이동 목표 위치

    private void Awake()
    {
        _targetPosition = transform.position;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        StateCheck();
    }

    private void StateCheck()
    {
        if (_currentState == EnemyState.Damaged || _currentState == EnemyState.Die)
        {
            return;
        }
        
        switch (_currentState)
        {
            case EnemyState.Idle:
            {
                Idle();
                break;
            }
            case EnemyState.Patrol:
            {
                Patrol();
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
        }
    }

    public void TakeDamage(Damage damage)
    {
        if (_currentState == EnemyState.Damaged || _currentState == EnemyState.Die)
        {
            return;
        }

        _health -= damage.Value;

        if (_health <= 0)
        {
            Debug.Log($"상태전환 {_currentState}->Die");
            _currentState = EnemyState.Die;
            StartCoroutine(Die_Coroutine());
            return;
        }

        Debug.Log($"상태전환 {_currentState}->Damaged");
        _currentState = EnemyState.Damaged;
        StartCoroutine(Damaged_Coroutine());
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
            _patrolTimer = 0f;
            return;
        }
        
        _patrolTimer += Time.deltaTime;
        if (_patrolTimer >= _patrolTime)
        {
            // 순찰
            Debug.Log("상태전환 Idle->Patrol");
            _patrolTimer = 0f;
            // 목표 위치 설정
            _targetPosition = _patrolPoints[Random.Range(0, _patrolPoints.Count)].position;
            _currentState = EnemyState.Patrol;
        }
    }

    private void Patrol()
    {
        // 전이 Trace
        if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
        {
            Debug.Log("상태전환 Patrol->Trace");
            _currentState = EnemyState.Trace;
            _patrolTimer = 0f;
            return;
        }
        
        // 전이 Idle
        if (Vector3.Distance(transform.position, _targetPosition) < _onPlaceThreshold)
        {
            Debug.Log("상태전환 Patrol->Idle");
            _currentState = EnemyState.Idle;
            return;
        }
        
        // 목표 지점으로 이동
        Vector3 direction = (_targetPosition - transform.position).normalized;
        _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
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
        if (Vector3.Distance(transform.position, _targetPosition) < _onPlaceThreshold)
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
        Vector3 direction = (_targetPosition - transform.position).normalized;
        _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
    }

    private void Attack()
    {
        // 전이 Trace
        if (Vector3.Distance(transform.position, _player.transform.position) >= _attackDistance)
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

    private IEnumerator Damaged_Coroutine()
    {
        yield return new WaitForSeconds(_stiffTime);
        Debug.Log("상태전환 Damaged->Trace");
        _currentState = EnemyState.Trace;
    }

    private IEnumerator Die_Coroutine()
    {
        // 사망
        yield return new WaitForSeconds(_dieTime);
        gameObject.SetActive(false);
    }
}