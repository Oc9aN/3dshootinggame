using System;
using UnityEngine;

public class WeaponPresenter : MonoBehaviour
{
    private Weapon _weapon;
    
    [SerializeField]
    private UI_PlayerWeapon _playerWeaponUI;
    
    [SerializeField]
    private UI_PlayerReload _playerReloadUI;

    private void Start()
    {
        WeaponManager.Instance.OnWeaponChanged += OnWeaponChanged;
        ViewManager.Instance.OnViewChanged += OnViewChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        _weapon = weapon;
        _weapon.OnAmmoChanged += _playerWeaponUI.RefreshAmmoText;
        _weapon.OnReloadProgressChanged += _playerReloadUI.RefreshSliderValue;
        
        _playerWeaponUI.RefreshAmmoText(_weapon.CurrentAmmo, _weapon.Data.MaxAmmo);
        _playerReloadUI.Initialize(_weapon.Data.ReloadTime);
        
        _playerWeaponUI.RefreshWeaponImage(_weapon.Data.WeaponSprite);
        _playerWeaponUI.RefreshCrosshairImage(_weapon.Data.CrosshairSprite);
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