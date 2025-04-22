using UnityEngine;

[CreateAssetMenu(fileName = "SO_Player", menuName = "Scriptable Objects/SO_Player")]
public class SO_Player : ScriptableObject
{
    // 기본 값을 관리
    [Header("Move")]
    [SerializeField]
    private float _gravity = -9.8f;
    public float Gravity => _gravity;
    
    [SerializeField]
    private float _defaultMoveSpeed = 7f;
    public float DefaultMoveSpeed => _defaultMoveSpeed;
    
    [SerializeField]
    private float _rotateSpeed = 90f;
    public float RotateSpeed => _rotateSpeed;
    
    [Header("Run")]
    [SerializeField]
    private float _runMoveSpeed = 12f;
    public float RunMoveSpeed => _runMoveSpeed;
    [SerializeField]
    private float _useStaminaPerSecond = 10f;
    public float UseStaminaPerSecond => _useStaminaPerSecond;
    
    [Header("Jump")]
    [SerializeField]
    private float _jumpForce = 5f;
    public float JumpForce => _jumpForce;
    
    [Header("Stamina")]
    [SerializeField]
    private float _maxStamina = 100f;
    public float MaxStamina => _maxStamina;
    
    [SerializeField]
    private float _staminaRecoverPerSecond = 20f;
    public float  StaminaRecoverPerSecond => _staminaRecoverPerSecond;
    
    [Header("Dash")]
    [SerializeField]
    private float _dashTime = 0.5f;
    public float DashTime => _dashTime;

    [SerializeField]
    private float _dashStaminaCost = 20f;
    public float DashStaminaCost => _dashStaminaCost;
    
    [SerializeField]
    private float _dashForce = 10f;
    public float DashForce => _dashForce;
    
    [Header("Climbing")]
    [SerializeField]
    private float _climbStaminaPerSecond = 10f;
    public float ClimbStaminaPerSecond => _climbStaminaPerSecond;

    [SerializeField]
    private float _climbForce = 11f;
    public float ClimbForce => _climbForce;
    
    [Header("Weapon")]
    [SerializeField]
    private int _maxAmmo = 50;
    public int MaxAmmo => _maxAmmo;

    [SerializeField]
    private int _maxBomb = 3;
    public int MaxBomb => _maxBomb;
}