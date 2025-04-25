using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 목표: 코드에서 변하는 플레이어 데이터 관리
    // TODO: 이동 방향과 Y가속도도 여기서 관리
    [Header("Data")]
    [SerializeField]
    private SO_Player _data;
    public SO_Player Data => _data;
    
    [SerializeField]
    private Weapon _currentWeapon;

    public event Action<Weapon> OnCurrentWeaponChanged;
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
    public float YVelocity { get => _yVelocity; set => _yVelocity = value; }

    private Vector3 _direction;
    public Vector3 Direction { get => _direction; set => _direction = value; }

    // 상태
    // 중력 적용?
    private bool _applyGravity = true;
    public bool ApplyGravity { get => _applyGravity; set => _applyGravity = value; }

    // 움직임 제한?
    private bool _isMoveable = true;
    public bool IsMoveable { get => _isMoveable; set => _isMoveable = value; }

    // 스테미나 회복?
    private bool _isRecoverStamina = true;
    public bool IsRecoverStamina { set => _isRecoverStamina = value; }

    // UI에 표시되는 값
    // 폭탄
    private int _bombCount = 3;
    public event Action<int, int> OnBombCountChanged;
    public int BombCount
    {
        get => _bombCount;
        set
        {
            _bombCount = value;
            OnBombCountChanged?.Invoke(_bombCount, _data.MaxBomb);
        }
    }

    // 스테미나
    [SerializeField] // 디버깅
    private float _stamina = 100f;
    public event Action<float> OnStaminaChanged; // 스테미나가 변할 때(늘거나, 줄을 때) 호출
    private float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = value;
            OnStaminaChanged?.Invoke(_stamina);
        }
    }

    private void Start()
    {
        _stamina = _data.MaxStamina;
        _bombCount = _data.MaxBomb;
    }

    // 스테미나만 Player에서관리
    private void Update()
    {
        RecoverStamina();
    }

    private void RecoverStamina()
    {
        if (!_isRecoverStamina)
        {
            return;
        }

        Stamina += _data.StaminaRecoverPerSecond * Time.deltaTime;
        Stamina = Mathf.Min(Stamina, _data.MaxStamina);
    }

    public bool TryUseStamina(float value)
    {
        // 스테미나가 있다면 사용
        if (Stamina - value < 0)
        {
            return false;
        }

        // 스테미나 사용
        _isRecoverStamina = false;
        Stamina -= value;
        return true;
    }
}