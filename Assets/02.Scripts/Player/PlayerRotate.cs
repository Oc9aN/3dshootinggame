using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 90f;
    
    // 카메라 각도는 0도에서 시작한다. 밑에 변수는 그 값.
    private float _rotationX = 0f;
    
    private void Update()
    {
        // 1. 마우스 입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        
        // 2. 마우스 입력으로부터 회전 시킬 크기를 누적한다.
        _rotationX += mouseX * _rotateSpeed * Time.deltaTime;
        
        transform.eulerAngles = new Vector3(0f, _rotationX, 0f);
    }
}