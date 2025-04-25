using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHealth : MonoBehaviour
{
    private Slider _slider;
    
    private Camera _camera;
    
    private Canvas _canvas;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _canvas = transform.parent.GetComponent<Canvas>();
        _camera = Camera.main;
        
        _canvas.worldCamera = _camera;
    }

    private void Update()
    {
        transform.forward = _camera.transform.forward;
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
