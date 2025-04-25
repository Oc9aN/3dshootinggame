using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable, IPoolObject
{
    // 상태만 가지고 있고 인터페이스를 통해 Acting
    protected Dictionary<EEnemyState, IEnemyState> _enemyStates;
    private IEnemyState _currentState;
    private EEnemyState _currentStateEnum;

    private int _health;

    [SerializeField]
    private GameObject _target;

    public GameObject Target { get => _target; set => _target = value; }

    [SerializeField]
    private SO_Enemy _data;

    public SO_Enemy Data { get => _data; set => _data = value; }

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
        _characterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _health = Data.Health;
        _targetPosition = transform.position;
        _navMeshAgent.speed = _data.MoveSpeed;

        _enemyStates = new Dictionary<EEnemyState, IEnemyState>()
        {
            { EEnemyState.Idle, new EnemyIdle(this) },
            { EEnemyState.Patrol, new EnemyPatrol(this) },
            { EEnemyState.Trace, new EnemyTrace(this) },
            { EEnemyState.Return, new EnemyReturn(this) },
            { EEnemyState.Attack, new EnemyAttack(this) },
            { EEnemyState.Damaged, new EnemyDamaged(this) },
            { EEnemyState.Die, new EnemyDie(this) }
        };
    }

    private void Start()
    {
        ChangeState(EEnemyState.Idle);
    }

    private void Update()
    {
        _currentState.Acting();
    }
    
    public void Initialize()
    {
        _health = Data.Health;
        _targetPosition = transform.position;
        _navMeshAgent.speed = _data.MoveSpeed;
        ChangeState(EEnemyState.Idle);
    }

    public void ChangeState(EEnemyState state)
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
            ChangeState(EEnemyState.Die);
            return;
        }

        ChangeState(EEnemyState.Damaged);
    }
}