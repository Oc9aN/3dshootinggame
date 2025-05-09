using System;
using UnityEngine;

public class PlayerPresenter : PlayerComponent
{
    // 플레이어 데이터와 UI간 연결
    [SerializeField]
    private UI_PlayerStamina _playerStaminaUI;
    
    [SerializeField]
    private UI_PlayerHealth _playerHealthUI;
    
    [SerializeField]
    private UI_PlayerBomb _playerBombUI;
    
    // TODO: 무기 UI는 무기 Presenter가
    [SerializeField]
    private UI_PlayerWeapon _playerWeaponUI;
    
    [SerializeField]
    private UI_PlayerReload _playerReloadUI;

    private void Start()
    {
        Player.OnStaminaChanged += _playerStaminaUI.RefreshSliderValue;
        Player.OnHealthChanged += _playerHealthUI.RefreshSliderValue;
        Player.OnBombCountChanged += _playerBombUI.RefreshBombText;
        Player.OnCurrentWeaponChanged += OnCurrentWeaponChanged;
        Player.OnDamaged += _playerHealthUI.OnDamage;
        
        ViewManager.Instance.OnViewChanged += OnViewChanged;
        
        _playerStaminaUI.Initialize(Player.Data.MaxStamina);
        _playerHealthUI.Initialize(Player.Data.MaxHealth);
        _playerBombUI.Initialize(Player.Data.MaxBomb);
    }

    // TODO: 무기 UI는 무기 Presenter가
    private void OnCurrentWeaponChanged(Weapon weapon)
    {
        weapon.OnAmmoChanged += _playerWeaponUI.RefreshAmmoText;
        weapon.OnReloadProgressChanged += _playerReloadUI.RefreshSliderValue;
        
        _playerWeaponUI.Initialize(Player.CurrentWeapon.Data.MaxAmmo);
        _playerReloadUI.Initialize(Player.CurrentWeapon.Data.ReloadTime);
        
        _playerWeaponUI.RefreshWeaponImage(Player.CurrentWeapon.Data.WeaponSprite);
        _playerWeaponUI.RefreshCrosshairImage(Player.CurrentWeapon.Data.CrosshairSprite);
    }

    private void OnViewChanged(EViewType viewType)
    {
        if (viewType == EViewType.FirstPerson || viewType == EViewType.ThirdPerson)
        {
            _playerWeaponUI.ActiveCrosshair(true);
        }
        else
        {
            _playerWeaponUI.ActiveCrosshair(false);
        }
    }
}
