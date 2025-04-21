using System;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    // 카메라 회전 스크립트
    // 목표: 마우스를 조작하면 카메라를 그 방향으로 회전시키기
    // 구현 순서
    // 1. 마우스 입력을 받는다.
    // 2. 마우스 입력으로부터 회전 시킬 크기를 누적한다.
    // 3. 카메라를 그 방향으로 회전시킨다.
    
    [SerializeField]
    private float _rotateSpeed = 90f;

    // 카메라 각도는 0도에서 시작한다. 밑에 변수는 그 값.
    private float _rotationX = 0f;
    private float _rotationY = 0f;
    
    private void Update()
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        // 2. 마우스 입력으로부터 회전 시킬 크기를 누적한다.
        _rotationX += mouseX * _rotateSpeed * Time.deltaTime;
        _rotationY += mouseY * _rotateSpeed * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);
        
        transform.eulerAngles = new Vector3(-_rotationY, _rotationX, 0f);
    }
}
