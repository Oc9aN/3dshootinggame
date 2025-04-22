using System;
using UnityEngine;

public class PlayerPresenter : PlayerComponent
{
    // 플레이어 데이터와 UI간 연결
    // TODO: UI와 인터페이스로 연결?
    [SerializeField]
    private UI_PlayerStamina _playerStamina;

    private void Start()
    {
        Player.OnStaminaChanged += OnStaminaChanged;
        _playerStamina.Initialize(Player.Data.MaxStamina);
    }

    private void OnStaminaChanged(float value)
    {
        _playerStamina.RefreshSliderValue(value);
    }
}
