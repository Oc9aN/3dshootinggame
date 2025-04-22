using System;
using UnityEngine;

public class PlayerPresenter : PlayerComponent
{
    // 플레이어 데이터와 UI간 연결
    // TODO: UI와 인터페이스로 연결?
    [SerializeField]
    private UI_PlayerStamina _playerStaminaUI;
    
    [SerializeField]
    private UI_PlayerBomb _playerBombUI;
    
    [SerializeField]
    private UI_PlayerAmmo _playerAmmoUI;
    
    [SerializeField]
    private UI_PlayerReload _playerReloadUI;

    private void Start()
    {
        Player.OnStaminaChanged += OnStaminaChanged;
        Player.OnBombCountChanged += OnBombCountChanged;
        Player.OnAmmoChanged += OnAmmoChanged;
        Player.OnReloadProgressChanged += OnReloadProgressChanged;
        
        _playerStaminaUI.Initialize(Player.Data.MaxStamina);
        _playerBombUI.Initialize(Player.Data.MaxBomb);
        _playerAmmoUI.Initialize(Player.Data.MaxAmmo);
        _playerReloadUI.Initialize(Player.Data.ReloadTime);
    }

    private void OnStaminaChanged(float value)
    {
        _playerStaminaUI.RefreshSliderValue(value);
    }

    private void OnBombCountChanged(int bombCount, int maxBombCount)
    {
        _playerBombUI.RefreshBombText(bombCount, maxBombCount);
    }
    
    private void OnAmmoChanged(int ammo, int maxAmmo)
    {
        _playerAmmoUI.RefreshAmmoText(ammo, maxAmmo);
    }

    private void OnReloadProgressChanged(float time)
    {
        _playerReloadUI.RefreshSliderValue(time);
    }
}
