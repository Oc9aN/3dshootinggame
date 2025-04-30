using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField]
    private Transform _firstViewTarget;

    [SerializeField]
    private Transform _player;

    private Transform _target;

    private ICameraViewStrategy _cameraViewStrategy;

    private CameraFirstView _cameraFirstView;
    private CameraThirdView _cameraThirdView;
    private CameraQuarterView _cameraQuarterView;

    private void Start()
    {
        _cameraFirstView = new CameraFirstView(transform, _firstViewTarget, _player);
        _cameraThirdView = new CameraThirdView(transform, _player);
        _cameraQuarterView = new CameraQuarterView(transform, _player);
        _cameraViewStrategy = _cameraFirstView;
        _cameraViewStrategy.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _cameraViewStrategy = _cameraFirstView;
            _cameraViewStrategy.Initialize();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _cameraViewStrategy = _cameraThirdView;
            _cameraViewStrategy.Initialize();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _cameraViewStrategy = _cameraQuarterView;
            _cameraViewStrategy.Initialize();
        }
        _cameraViewStrategy.View();
    }
}