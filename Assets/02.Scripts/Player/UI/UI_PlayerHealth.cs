using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Image _damagedEffect;
    
    [SerializeField]
    private float _damagedEffectFadeOutTime = 1f;

    [SerializeField]
    private Slider _healthSlider;
    
    private IEnumerator damage_Coroutine;

    public void Initialize(float maxSliderValue)
    {
        _healthSlider.maxValue = maxSliderValue;
        _healthSlider.value = maxSliderValue;
        _damagedEffect.color = new Color(1, 1, 1, 0);
    }

    public void RefreshSliderValue(float value)
    {
        _healthSlider.value = value;
    }

    public void OnDamage()
    {
        _damagedEffect.color = Color.white;
        if (!ReferenceEquals(damage_Coroutine, null))
        {
            StopCoroutine(damage_Coroutine);
        }
        damage_Coroutine = Damage_Coroutine();
        StartCoroutine(damage_Coroutine);
    }

    private IEnumerator Damage_Coroutine()
    {
        Color newColor = Color.white;
        while (newColor.a >= 0)
        {
            newColor.a -= _damagedEffectFadeOutTime * Time.deltaTime;
            _damagedEffect.color = newColor;
            yield return null;
        }

        newColor.a = 0f;
        _damagedEffect.color = newColor;
    }
}