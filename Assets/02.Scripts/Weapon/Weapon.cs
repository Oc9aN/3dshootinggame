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

    protected Vector3 _currentRecoil;
    protected Vector3 _targetRecoil;

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

    // 상태
    private bool _isReloading = false;

    public bool IsReloading
    {
        get => _isReloading;
        set
        {
            _isReloading = value;
            if (!_isReloading)
                ReloadingProgress = 0f;
        }
    }

    // 재장전 진행도
    private float _reloadingProgress = 0f;

    public float ReloadingProgress
    {
        get => _reloadingProgress;
        set
        {
            _reloadingProgress = value;
            OnReloadProgressChanged?.Invoke(_reloadingProgress);
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

    private void Update()
    {
        _fireRate -= Time.deltaTime;
    }

    // 공격 방식대로 공격
    public abstract void Attack();

    protected void ApplyRandomRecoil()
    {
        float vertical = Random.Range(0f, _data.VerticalRecoil);                          // 위로 튕김
        float horizontal = Random.Range(-_data.HorizontalRecoil, _data.HorizontalRecoil); // 좌우 랜덤
        _targetRecoil += new Vector3(vertical, horizontal, 0f);
    }

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