using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField]
    private Transform _firstViewTarget;
    
    [SerializeField]
    private Transform _thirdViewTarget;
    
    [SerializeField]
    private Transform _quarterViewTarget;
    
    private Transform _target;
    
    private ICameraViewStrategy _cameraViewStrategy;

    private void Start()
    {
        _cameraViewStrategy = new CameraFirstView();
        _target = _firstViewTarget;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _cameraViewStrategy = new CameraFirstView();
            _target = _firstViewTarget;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _cameraViewStrategy = new CameraThirdView();
            _target = _thirdViewTarget;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _cameraViewStrategy = new CameraQuarterView();
            _target = _quarterViewTarget;
        }
        _cameraViewStrategy.View(transform, _target);
    }
}
