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
    
    [SerializeField]
    private UI_PlayerAmmo _playerAmmoUI;
    
    [SerializeField]
    private UI_PlayerReload _playerReloadUI;

    private void Start()
    {
        Player.OnStaminaChanged += _playerStaminaUI.RefreshSliderValue;
        Player.OnHealthChanged += _playerHealthUI.RefreshSliderValue;
        Player.OnBombCountChanged += _playerBombUI.RefreshBombText;
        Player.OnCurrentWeaponChanged += OnCurrentWeaponChanged;
        
        _playerStaminaUI.Initialize(Player.Data.MaxStamina);
        _playerHealthUI.Initialize(Player.Data.MaxHealth);
        _playerBombUI.Initialize(Player.Data.MaxBomb);
    }

    private void OnCurrentWeaponChanged(Weapon weapon)
    {
        weapon.OnAmmoChanged += _playerAmmoUI.RefreshAmmoText;
        weapon.OnReloadProgressChanged += _playerReloadUI.RefreshSliderValue;
        
        _playerAmmoUI.Initialize(Player.CurrentWeapon.Data.MaxAmmo);
        _playerReloadUI.Initialize(Player.CurrentWeapon.Data.ReloadTime);
    }
}
