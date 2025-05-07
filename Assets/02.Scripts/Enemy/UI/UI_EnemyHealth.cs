using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_EnemyHealth : MonoBehaviour
{
    private const float DELAY_SPEED = 20f; // 딜레이 감소 속도 조절 (초당 감소량)
    
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Slider _delaySlider; // 딜레이 슬라이더

    private Camera _camera;
    private Canvas _canvas;


    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _camera = Camera.main;

        _canvas.worldCamera = _camera;

        // 초기화 시 딜레이 슬라이더 값도 설정
        if (_delaySlider != null)
        {
            _delaySlider.maxValue = _slider.maxValue;
            _delaySlider.value = _slider.value;
        }
    }

    private void Update()
    {
        transform.forward = _camera.transform.forward;

        // 딜레이 슬라이더 값을 현재 슬라이더 값으로 부드럽게 감소시킴
        if (_delaySlider != null && _delaySlider.value > _slider.value)
        {
            _delaySlider.value -= DELAY_SPEED * Time.deltaTime;
        }
        // 현재 슬라이더 값이 딜레이 슬라이더 값보다 커지는 경우 즉시 동기화
        else if (_delaySlider != null && _slider.value > _delaySlider.value)
        {
            _delaySlider.value = _slider.value;
        }
    }

    public void Initialize(float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
        if (_delaySlider != null)
        {
            _delaySlider.maxValue = maxValue;
            _delaySlider.value = maxValue;
        }
    }

    public void RefreshSliderValue(float value)
    {
        _slider.value = value;
        // 체력이 감소했을 때만 딜레이 슬라이더 값을 현재 값으로 설정하여 딜레이 효과 시작
        if (_delaySlider != null && _delaySlider.value > _slider.value)
        {
            // 이미 Update 함수에서 서서히 감소시키므로 여기서는 값을 바로 설정만 함
        }
        else if (_delaySlider != null && _delaySlider.value < _slider.value)
        {
            _delaySlider.value = _slider.value;
        }
    }
}