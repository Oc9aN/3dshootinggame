using UnityEngine;

public class CameraFirstView : ICameraViewStrategy
{
    private const float ROTATE_SPEED = 90f;

    // 카메라 각도는 0도에서 시작한다. 밑에 변수는 그 값.
    private float _rotationX = 0f;
    private float _rotationY = 0f;
    
    private Transform _camera;
    private Transform _target;
    private Transform _player;
    
    public CameraFirstView(Transform camera, Transform target, Transform player)
    {
        _camera = camera;
        _target = target;
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
        _camera.transform.position = _target.position;
    }
}
