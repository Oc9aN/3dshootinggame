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

    private void Start()
    {
        Player.OnStaminaChanged += _playerStaminaUI.RefreshSliderValue;
        Player.OnHealthChanged += _playerHealthUI.RefreshSliderValue;
        Player.OnBombCountChanged += _playerBombUI.RefreshBombText;
        Player.OnDamaged += _playerHealthUI.OnDamage;
        
        _playerStaminaUI.Initialize(Player.Data.MaxStamina);
        _playerHealthUI.Initialize(Player.Data.MaxHealth);
        _playerBombUI.Initialize(Player.Data.MaxBomb);
    }
}
