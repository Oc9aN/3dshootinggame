using System;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    // 목표: 코드에서 변하는 플레이어 데이터 관리
    public event Action<Weapon> OnCurrentWeaponChanged;
    public event Action<int, int> OnBombCountChanged;
    public event Action<float> OnStaminaChanged; // 스테미나가 변할 때(늘거나, 줄을 때) 호출
    public event Action<float> OnHealthChanged;
    public event Action OnDamaged;

    [Header("Data")]
    [SerializeField]
    private SO_Player _data;

    public SO_Player Data => _data;

    // TODO: 무기와 의존성 분리
    private Weapon _currentWeapon;

    public Weapon CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon?.SwapWeapon();
            _currentWeapon = value;
            OnCurrentWeaponChanged?.Invoke(_currentWeapon);
            _currentWeapon.SetWeapon();
        }
    }

    [SerializeField] // 디버깅
    private float _moveSpeed = 7f;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    private float _yVelocity = 0f;
    public float YVelocity { get => _yVelocity; set { _yVelocity = value; } }

    private Vector3 _direction;
    public Vector3 Direction { get => _direction; set => _direction = value; }

    private Animator _animator;
    public Animator Animator => _animator;

    // 상태
    // 중력 적용?
    private bool _applyGravity = true;
    public bool ApplyGravity { get => _applyGravity; set => _applyGravity = value; }

    // 움직임 제한?
    private bool _isMoveable = true;
    public bool IsMoveable { get => _isMoveable; set => _isMoveable = value; }

    // 스테미나 회복?
    private bool _isRecoverStamina = true;
    public bool IsRecoverStamina { get => _isRecoverStamina; set => _isRecoverStamina = value; }

    // UI에 표시되는 값
    // 폭탄
    private int _currentBombCount = 3;

    public int BombCount
    {
        get => _currentBombCount;
        set
        {
            _currentBombCount = value;
            OnBombCountChanged?.Invoke(_currentBombCount, _data.MaxBomb);
        }
    }

    // 스테미나
    [SerializeField] // 디버깅
    private float _currentCurrentStamina = 100f;

    public float CurrentStamina
    {
        get => _currentCurrentStamina;
        set
        {
            _currentCurrentStamina = value;
            OnStaminaChanged?.Invoke(_currentCurrentStamina);
        }
    }

    private float _currentHealth;

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            // _animator.SetLayerWeight(_animator.GetLayerIndex("Injured Layer"), 1 - (_currentHealth / _data.MaxHealth));
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _currentCurrentStamina = _data.MaxStamina;
        _currentBombCount = _data.MaxBomb;
        _currentHealth = _data.MaxHealth;
    }

    // 사용 함수만 관리
    public bool TryUseStamina(float value)
    {
        // 스테미나가 있다면 사용
        if (CurrentStamina - value < 0)
        {
            return false;
        }

        // 스테미나 사용
        _isRecoverStamina = false;
        CurrentStamina -= value;
        return true;
    }

    public void TakeDamage(Damage damage)
    {
        Debug.Log("TakeDamage" + damage.From + ": " + damage.DamageValue);
        CurrentHealth -= damage.DamageValue;
        OnDamaged?.Invoke();

        if (CurrentHealth <= 0)
        {
            GameManager.Instance.GameState = EGameState.Over;
        }
    }
}