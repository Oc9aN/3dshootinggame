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

    public void Initialize(float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
    }

    public void RefreshSliderValue(float value)
    {
        _slider.value = value;
    }
}
