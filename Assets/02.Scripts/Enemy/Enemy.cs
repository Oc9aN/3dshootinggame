using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // TODO: 상태만 가지고 있고 인터페이스를 통해 Acting
    private Dictionary<EnemyState, IEnemyState> _enemyStates;
    private IEnemyState _currentState;
    private EnemyState _currentStateEnum;

    private int _health;

    [SerializeField]
    private GameObject _player;
    public GameObject Player => _player;

    [SerializeField]
    private SO_Enemy _data;
    public SO_Enemy Data => _data;

    [SerializeField]
    private List<Transform> _patrolPoints;
    public List<Transform> PatrolPoints => _patrolPoints;

    private Vector3 _targetPosition;
    public Vector3 TargetPosition { get => _targetPosition; set => _targetPosition = value; }

    private CharacterController _characterController;
    public CharacterController CharacterController => _characterController;

    private float _knockBackForce;
    public float KnockBackForce => _knockBackForce;

    private void Awake()
    {
        _health = Data.Health;
        _targetPosition = transform.position;
        _characterController = GetComponent<CharacterController>();

        _enemyStates = new Dictionary<EnemyState, IEnemyState>()
        {
            { EnemyState.Idle, new EnemyIdle(this) },
            { EnemyState.Patrol, new EnemyPatrol(this) },
            { EnemyState.Trace, new EnemyTrace(this) },
            { EnemyState.Return, new EnemyReturn(this) },
            { EnemyState.Attack, new EnemyAttack(this) },
            { EnemyState.Damaged, new EnemyDamaged(this) },
            { EnemyState.Die, new EnemyDie(this) }
        };
    }

    private void Start()
    {
        ChangeState(EnemyState.Idle);
    }

    private void Update()
    {
        _currentState.Acting();
    }

    public void ChangeState(EnemyState state)
    {
        Debug.Log($"상태전환 {_currentStateEnum}->{state}");
        _currentStateEnum = state;
        
        _currentState?.Exit();
        _currentState = _enemyStates[_currentStateEnum];
        _currentState.Enter();
    }

    public void StartEnemyStateCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
    
    public void StopEnemyStateCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }

    public void TakeDamage(Damage damage)
    {
        _health -= damage.DamageValue;
        _knockBackForce = damage.KnockBackForce;
        if (_health <= 0)
        {
            ChangeState(EnemyState.Die);
            return;
        }
        ChangeState(EnemyState.Damaged);
    }

    // private void Update()
    // {
    //     StateCheck();
    // }
    //
    // private void StateCheck()
    // {
    //     if (_currentState == EnemyState.Damaged || _currentState == EnemyState.Die)
    //     {
    //         return;
    //     }
    //
    //     switch (_currentState)
    //     {
    //         case EnemyState.Idle:
    //         {
    //             Idle();
    //             break;
    //         }
    //         case EnemyState.Patrol:
    //         {
    //             Patrol();
    //             break;
    //         }
    //         case EnemyState.Trace:
    //         {
    //             Trace();
    //             break;
    //         }
    //         case EnemyState.Return:
    //         {
    //             Return();
    //             break;
    //         }
    //         case EnemyState.Attack:
    //         {
    //             Attack();
    //             break;
    //         }
    //     }
    // }
    //
    // public void TakeDamage(Damage damage)
    // {
    //     if (_currentState == EnemyState.Die)
    //     {
    //         return;
    //     }
    //
    //     _health -= damage.DamageValue;
    //
    //     if (_health <= 0)
    //     {
    //         Debug.Log($"상태전환 {_currentState}->Die");
    //         _currentState = EnemyState.Die;
    //         StartCoroutine(Die_Coroutine());
    //         return;
    //     }
    //
    //     Debug.Log($"상태전환 {_currentState}->Damaged");
    //     _currentState = EnemyState.Damaged;
    //     if (_damageCoroutine != null)
    //         StopCoroutine(_damageCoroutine);
    //     _damageCoroutine = StartCoroutine(Damaged_Coroutine(damage.KnockBackForce));
    // }
    //
    // // FSM
    // private void Idle()
    // {
    //     // 대기 상태
    //     // 전이 Trace
    //     if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
    //     {
    //         Debug.Log("상태전환 Idle->Trace");
    //         _currentState = EnemyState.Trace;
    //         _patrolTimer = 0f;
    //         return;
    //     }
    //
    //     _patrolTimer += Time.deltaTime;
    //     if (_patrolTimer >= _patrolTime)
    //     {
    //         // 순찰
    //         Debug.Log("상태전환 Idle->Patrol");
    //         _patrolTimer = 0f;
    //         // 목표 위치 설정
    //         _targetPosition = _patrolPoints[Random.Range(0, _patrolPoints.Count)].position;
    //         _currentState = EnemyState.Patrol;
    //     }
    // }
    //
    // private void Patrol()
    // {
    //     // 전이 Trace
    //     if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
    //     {
    //         Debug.Log("상태전환 Patrol->Trace");
    //         _currentState = EnemyState.Trace;
    //         _patrolTimer = 0f;
    //         return;
    //     }
    //
    //     // 전이 Idle
    //     if (Vector3.Distance(transform.position, _targetPosition) < _onPlaceThreshold)
    //     {
    //         Debug.Log("상태전환 Patrol->Idle");
    //         _currentState = EnemyState.Idle;
    //         return;
    //     }
    //
    //     // 목표 지점으로 이동
    //     Vector3 direction = (_targetPosition - transform.position).normalized;
    //     _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
    // }
    //
    // private void Trace()
    // {
    //     // 전이 Return
    //     if (Vector3.Distance(transform.position, _player.transform.position) >= _findDistance)
    //     {
    //         Debug.Log("상태전환 Trace->Return");
    //         _currentState = EnemyState.Return;
    //         return;
    //     }
    //
    //     // 전이 Attack
    //     if (Vector3.Distance(transform.position, _player.transform.position) < _attackDistance)
    //     {
    //         Debug.Log("상태전환 Trace->Attack");
    //         _currentState = EnemyState.Attack;
    //         return;
    //     }
    //
    //     // 플레이어 추적
    //     Vector3 direction = (_player.transform.position - transform.position).normalized;
    //     _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
    // }
    //
    // private void Return()
    // {
    //     // 전이 Idle
    //     if (Vector3.Distance(transform.position, _targetPosition) < _onPlaceThreshold)
    //     {
    //         Debug.Log("상태전환 Return->Idle");
    //         _currentState = EnemyState.Idle;
    //         return;
    //     }
    //
    //     // 전이 Trace
    //     if (Vector3.Distance(transform.position, _player.transform.position) < _findDistance)
    //     {
    //         Debug.Log("상태전환 Return->Trace");
    //         _currentState = EnemyState.Trace;
    //         return;
    //     }
    //
    //     // 제자리로 복귀
    //     Vector3 direction = (_targetPosition - transform.position).normalized;
    //     _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
    // }
    //
    // private void Attack()
    // {
    //     // 전이 Trace
    //     if (Vector3.Distance(transform.position, _player.transform.position) >= _attackDistance)
    //     {
    //         Debug.Log("상태전환 Attack->Trace");
    //         _attackCoolTimer = 0f;
    //         _currentState = EnemyState.Trace;
    //         return;
    //     }
    //
    //     // 공격
    //     _attackCoolTimer += Time.deltaTime;
    //     if (_attackCoolTimer >= _attackCoolTime)
    //     {
    //         Debug.Log("Attack");
    //         _attackCoolTimer = 0f;
    //     }
    // }
    //
    // private IEnumerator Damaged_Coroutine(float knockBackForce)
    // {
    //     float startTime = Time.time;
    //     float currentKnockBackValue = knockBackForce;
    //     while (Time.time - startTime < 1f)
    //     {
    //         // 경과 시간 비율 (0 ~ 1)
    //         float timeRatio = (Time.time - startTime) / 1f;
    //
    //         // 감소된 넉백 값 (1에서 0으로 선형 감소)
    //         currentKnockBackValue = knockBackForce * (1f - timeRatio);
    //
    //         Vector3 direction = (transform.position - _player.transform.position).normalized;
    //         _characterController.Move(direction * (currentKnockBackValue * Time.deltaTime));
    //         yield return null;
    //     }
    //
    //     Debug.Log("상태전환 Damaged->Trace");
    //     _currentState = EnemyState.Trace;
    // }
    //
    // private IEnumerator Die_Coroutine()
    // {
    //     // 사망
    //     yield return new WaitForSeconds(_dieTime);
    //     gameObject.SetActive(false);
    // }
}