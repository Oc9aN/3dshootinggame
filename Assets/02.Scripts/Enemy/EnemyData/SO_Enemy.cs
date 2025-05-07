using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_Enemy : ScriptableObject
{
    [SerializeField]
    private int _maxHealth;
    public int MaxHealth => _maxHealth;

    [SerializeField]
    private float _findDistance = 7f; // 추격 거리
    public float FindDistance => _findDistance;

    [SerializeField]
    private float _attackDistance = 2.5f; // 공격 사거리

    public float AttackDistance => _attackDistance;

    [SerializeField]
    private float _onPlaceThreshold = 0.1f; // 제자리 임계점
    public float OnPlaceThreshold => _onPlaceThreshold;

    [SerializeField]
    private float _moveSpeed = 3.3f; // 이동 속도
    public float MoveSpeed => _moveSpeed;

    [SerializeField]
    private float _attackCoolTime = 2f; // 공격 쿨타임

    public float AttackCoolTime => _attackCoolTime;

    [SerializeField]
    private float _damagedTime = 0.5f; // 경직 시간
    public float DamagedTime => _damagedTime;

    [SerializeField]
    private float _dieTime = 2f; // 사망 시간

    public float DieTime => _dieTime;
    
    [SerializeField]
    private float _patrolTime = 1f;
    public float PatrolTime => _patrolTime;
    
    [SerializeField]
    private Damage _damage;
    public Damage Damage => _damage;

    [SerializeField]
    private EEnemyType _enemyType;
    public EEnemyType EnemyType => _enemyType;
}
