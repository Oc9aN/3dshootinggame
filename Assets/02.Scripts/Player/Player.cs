using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 목표: 코드에서 변하는 플레이어 데이터 관리
    [Header("Movement")]
    [SerializeField]
    private float _defaultMoveSpeed = 7f;

    [SerializeField]
    private float _moveSpeed = 7f;

    public float MoveSpeed => _moveSpeed;

    [SerializeField]
    private float _rotateSpeed = 90f;

    public float RotateSpeed => _rotateSpeed;

    [Header("Stamina")]
    [SerializeField]
    private float _maxStamina = 100f;

    public float MaxStamina => _maxStamina;

    [SerializeField]
    private float _staminaRecoverPerSecond = 20f;

    private float _stamina = 100f;

    private float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = value;
            OnStaminaChanged?.Invoke(_stamina);
        }
    }

    private bool _isRecoverStamina = true;

    public bool IsRecoverStamina
    {
        set => _isRecoverStamina = value;
    }

    // 대쉬
    private bool _isDash = false;

    public bool IsDash
    {
        get => _isDash;
        set => _isDash = value;
    }

    public event Action<float> OnStaminaChanged; // 스테미나가 변할 때(늘거나, 줄을 때) 호출

    private void Awake()
    {
        Stamina = _maxStamina;
    }

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

        Stamina += _staminaRecoverPerSecond * Time.deltaTime;
        Stamina = Mathf.Min(Stamina, _maxStamina);
    }

    public bool TryUseStamina(float value)
    {
        // 스테미나가 있다면 사용
        if (Stamina - value < 0)
        {
            Debug.Log("Stamina is negative");
            //_isUsingStamina = false;
            return false;
        }

        // 스테미나 사용
        _isRecoverStamina = false;
        Stamina -= value;
        return true;
    }

    public void SetMoveSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    public void SetDefaultMoveSpeed()
    {
        _moveSpeed = _defaultMoveSpeed;
    }
}