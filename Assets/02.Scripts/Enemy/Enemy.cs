using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    // 상태만 가지고 있고 인터페이스를 통해 Acting
    protected Dictionary<EnemyState, IEnemyState> _enemyStates;
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
    
    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private Damage _damageInfo;
    public Damage DamageInfo => _damageInfo;

    private void Awake()
    {
        _health = Data.Health;
        _targetPosition = transform.position;
        _characterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _data.MoveSpeed;

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
        _damageInfo = damage;
        if (_health <= 0)
        {
            ChangeState(EnemyState.Die);
            return;
        }
        ChangeState(EnemyState.Damaged);
    }
}