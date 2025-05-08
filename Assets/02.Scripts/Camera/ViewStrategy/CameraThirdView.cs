using System;
using UnityEngine;

public class CameraThirdView : ICameraViewStrategy
{
    private const float ROTATE_SPEED = 90f;
    private const float CAMERA_DISTANCE = 3f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;

    private Transform _camera;
    private Transform _player;

    public CameraThirdView(Transform camera, Transform player)
    {
        _camera = camera;
        _player = player;
    }

    public void Reset()
    {
        //방향을 정렬한다.
        _rotationX = _player.rotation.eulerAngles.y;
        _rotationY = _player.rotation.eulerAngles.x;
    }

    public void View()
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = InputHandler.GetAxis("Mouse X");
        float mouseY = InputHandler.GetAxis("Mouse Y");

        // 2. 마우스 입력으로부터 회전 시킬 크기를 누적한다.
        _rotationX += mouseX * ROTATE_SPEED * Time.deltaTime;
        _rotationY += mouseY * ROTATE_SPEED * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        _camera.eulerAngles = new Vector3(-_rotationY, _rotationX, 0f);
        Vector3 Shoulder = _player.right * 1f + _player.up * 1f;
        _camera.position = _player.position + Shoulder + -_camera.forward * CAMERA_DISTANCE;
    }
}