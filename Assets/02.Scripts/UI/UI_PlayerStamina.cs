using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStamina : MonoBehaviour
{
    // 플레이어의 스테미나를 Slider로 표시
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void Initialize(float maxStamina)
    {
        _slider.maxValue = maxStamina;
        _slider.value = maxStamina;
    }

    public void RefreshSliderValue(float value)
    {
        _slider.value = value;
    }
}
