using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    private Slider _healthSlider;
    
    private void Awake()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public void Initialize(float maxValue)
    {
        _healthSlider.maxValue = maxValue;
        _healthSlider.value = maxValue;
    }

    public void RefreshSliderValue(float value)
    {
        _healthSlider.value = value;
    }
}