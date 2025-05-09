using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    // 플레이어가 가지는 무기
    public event Action<float> OnReloadProgressChanged;
    public event Action AttackAnimationTrigger;
    public event Action<int, int> OnAmmoChanged;
    public event Action OnAttack;

    [SerializeField]
    protected SO_Weapon _data;

    [SerializeField]
    protected Transform _attackPosition;
    public Transform AttackPosition => _attackPosition;

    public SO_Weapon Data => _data;

    protected float _fireRate = 0f;

    protected Camera _camera;

    protected IWeaponAimStrategy _weaponAimStrategy;
    private CrosshairAimStrategy _crosshairAimStrategy;
    private CursorAimStrategy _cursorAimStrategy;

    private int _currentAmmo;

    public int CurrentAmmo
    {
        get => _currentAmmo;
        set
        {
            _currentAmmo = value;
            OnAmmoChanged?.Invoke(_currentAmmo, _data.MaxAmmo);
        }
    }

    protected virtual void Awake()
    {
        _camera = Camera.main;

        _crosshairAimStrategy = new CrosshairAimStrategy();
        _cursorAimStrategy = new CursorAimStrategy();

        _weaponAimStrategy = _crosshairAimStrategy;
    }

    private void Start()
    {
        ViewManager.Instance.OnViewChanged += OnViewChanged;

        CurrentAmmo = _data.MaxAmmo;
    }

    protected virtual void Update()
    {
        _fireRate -= Time.deltaTime;
    }

    // 공격 방식대로 공격
    public abstract void Attack();
    protected abstract void Reloading();

    public void SetWeapon()
    {
        _fireRate = 0f;
        gameObject.SetActive(true);
        transform.localPosition = Vector3.zero;
        CurrentAmmo = _currentAmmo;
    }

    public void SwapWeapon()
    {
        OnAmmoChanged = null;
        OnReloadProgressChanged = null;
        gameObject.SetActive(false);
        AttackAnimationTrigger = null;
    }

    protected void TriggerAnimation()
    {
        AttackAnimationTrigger?.Invoke();
    }

    protected void TriggerOnAttack()
    {
        OnAttack?.Invoke();
    }

    protected void TriggerOnReload(float progress)
    {
        OnReloadProgressChanged?.Invoke(progress);
    }

    private void OnViewChanged(EViewType viewType)
    {
        if (viewType == EViewType.FirstPerson || viewType == EViewType.ThirdPerson)
        {
            _weaponAimStrategy = _crosshairAimStrategy;
        }
        else
        {
            _weaponAimStrategy = _cursorAimStrategy;
        }
    }
}