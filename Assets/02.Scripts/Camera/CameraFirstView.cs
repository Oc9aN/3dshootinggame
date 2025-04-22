using UnityEngine;

public class CameraFirstView : ICameraViewStrategy
{
    private const float ROTATE_SPEED = 90f;

    // 카메라 각도는 0도에서 시작한다. 밑에 변수는 그 값.
    private float _rotationX = 0f;
    private float _rotationY = 0f;

    public void View(Transform camera, Transform target)
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        // 2. 마우스 입력으로부터 회전 시킬 크기를 누적한다.
        _rotationX += mouseX * ROTATE_SPEED * Time.deltaTime;
        _rotationY += mouseY * ROTATE_SPEED * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);
        
        camera.eulerAngles = new Vector3(-_rotationY, _rotationX, 0f);
        camera.transform.position = target.position;
    }
}
