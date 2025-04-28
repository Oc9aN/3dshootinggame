using System;
using UnityEngine;

public class CameraThirdView : ICameraViewStrategy
{
    private const float ROTATE_SPEED = 90f;
    private const float CAMERA_DISTANCE = 5f;
    
    private readonly Vector3 _positionMargin = new Vector3(1f, 1f, 0f);
    
    private float _rotationX = 0f;
    private float _rotationY = 0f;

    public void View(Transform camera, Transform target)
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = InputHandler.GetAxis("Mouse X");
        float mouseY = InputHandler.GetAxis("Mouse Y");
        
        // 2. 마우스 입력으로부터 회전 시킬 크기를 누적한다.
        _rotationX += mouseX * ROTATE_SPEED * Time.deltaTime;
        _rotationY += mouseY * ROTATE_SPEED * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);
        
        camera.eulerAngles = new Vector3(-_rotationY, _rotationX, 0f);
        camera.position = target.position + -camera.forward * CAMERA_DISTANCE + _positionMargin;
    }
}
