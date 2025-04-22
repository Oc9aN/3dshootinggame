using System;
using UnityEngine;

public class PlayerPresenter : PlayerComponent
{
    // 플레이어 데이터와 UI간 연결
    // TODO: UI와 인터페이스로 연결?
    [SerializeField]
    private UI_PlayerStamina _playerStamina;
    
    [SerializeField]
    private UI_PlayerBomb _bombCount;

    private void Start()
    {
        Player.OnStaminaChanged += OnStaminaChanged;
        Player.OnBombCountChanged += OnBombCountChanged;
        
        _playerStamina.Initialize(Player.Data.MaxStamina);
        _bombCount.Initialize(Player.Data.MaxBomb);
    }

    private void OnStaminaChanged(float value)
    {
        _playerStamina.RefreshSliderValue(value);
    }

    private void OnBombCountChanged(int bombCount, int maxBombCount)
    {
        _bombCount.RefreshBombText(bombCount, maxBombCount);
    }
}
