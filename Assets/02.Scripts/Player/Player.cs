using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 목표: 코드에서 변하는 플레이어 데이터 관리
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
    [SerializeField]
    private float _staminaRecoverPerSecond = 20f;
    
    private float _stamina = 100f;
    private bool _isUsingStamina = false;
    public bool IsUsingStamina { set => _isUsingStamina = value; }

    private void Update()
    {
        RecoverStamina();
    }

    private void RecoverStamina()
    {
        if (_isUsingStamina)
        {
            return;
        }

        _stamina += _staminaRecoverPerSecond * Time.deltaTime;
        _stamina = Mathf.Min(_stamina, _maxStamina);
        Debug.Log("Stamina: " + _stamina);
    }

    public bool TryUseStamina(float value)
    {
        // 스테미나가 있다면 사용
        if (_stamina - value < 0)
        {
            Debug.Log("Stamina is negative");
            _isUsingStamina = false;
            return false;
        }
        // 스테미나 사용
        _isUsingStamina = true;
        _stamina -= value;
        Debug.Log("Stamina: " + _stamina);
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
