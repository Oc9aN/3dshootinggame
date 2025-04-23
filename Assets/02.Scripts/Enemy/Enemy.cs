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
    private float _findDistance = 7f;   // 추격 거리

    [SerializeField]
    private float _attackDistance = 2.5f;
    
    private CharacterController _characterController;
    [SerializeField]
    private float _moveSpeed = 3.3f;

    private void Awake()
    {
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
                Return();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        // 대기 상태
        // 전이
        if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
        {
            Debug.Log($"상태전환 Idle->Trace");
            _currentState = EnemyState.Trace;
        }
    }

    private void Trace()
    {
        // 전이 Return
        if (Vector3.Distance(transform.position, _player.transform.position) >= _findDistance)
        {
            Debug.Log($"상태전환 Trace->Return");
            _currentState = EnemyState.Return;
            return;
        }
        // 전이 Attack
        if (Vector3.Distance(transform.position, _player.transform.position) < _attackDistance)
        {
            Debug.Log($"상태전환 Trace->Attack");
            _currentState = EnemyState.Attack;
            return;
        }
        
        // 플레이어 추적
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
    }

    private void Return()
    {
        // 제자리로 복귀
    }

    private void Attack()
    {
        // 공격
    }

    private void Damaged()
    {
        // 피격
    }

    private void Die()
    {
        // 사망
    }
}