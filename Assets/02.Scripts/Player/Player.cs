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
    
    // 장전중?
    private bool _isReloading = false;
    public bool IsReloading { get => _isReloading; set => _isReloading = value; }
    
    // UI에 표시되는 값
    private float _reloadingProgress = 0f;
    public event Action<float> OnReloadProgressChanged;

    public float ReloadingProgress
    {
        get => _reloadingProgress;
        set
        {
            _reloadingProgress = value;
            OnReloadProgressChanged?.Invoke(_reloadingProgress);
        }
    }

    private int _currentAmmo = 50;
    public event Action<int, int> OnAmmoChanged;
    private int CurrentAmmo
    {
        get => _currentAmmo;
        set
        {
            _currentAmmo = value;
            OnAmmoChanged?.Invoke(_currentAmmo, _data.MaxAmmo);
        }
    }
    
    private int _bombCount = 3;
    public event Action<int, int> OnBombCountChanged;
    private int BombCount
    {
        get => _bombCount;
        set
        {
            _bombCount = value;
            OnBombCountChanged?.Invoke(_bombCount, _data.MaxBomb);
        }
    }

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

    private void Awake()
    {
        _stamina = _data.MaxStamina;
        _currentAmmo = _data.MaxAmmo;
        _bombCount = _data.MaxBomb;
    }

    private void Update()
    {
        RecoverStamina();
        Reloading();
    }

    private void Reloading()
    {
        if (_isReloading)
        {
            ReloadingProgress += Time.deltaTime;
            if (_reloadingProgress >= _data.ReloadTime)
            {
                // 재장전
                CurrentAmmo = _data.MaxAmmo;
                _isReloading = false;
                ReloadingProgress = 0f;
            }
        }
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

    public bool TryUseBomb()
    {
        if (_bombCount <= 0)
        {
            return false;
        }

        BombCount--;
        return true;
    }

    public bool TryUseAmmo()
    {
        if (_currentAmmo <= 0)
        {
            return false;
        }

        if (_isReloading)
        {
            _isReloading = false;
            ReloadingProgress = 0f;
        }
        CurrentAmmo--;
        return true;
    }
}