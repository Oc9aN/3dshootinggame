using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 2. 오른쪽 버튼 입력 받기
    // 3. 발사 위치에 수류탄 생성하기
    // 4. 생성된 수류탄을 카메라 방향으로 물리적인 힘 가하기

    private GameObject _firePosition;

    private GameObject _bombPrefab;

    private void Update()
    {
        // 0: 왼쪽, 2: 오른쪽, 3: 휠
        if (Input.GetMouseButtonDown(1))
        {
            GameObject bomb = Instantiate(_bombPrefab);
            bomb.transform.position = _firePosition.transform.position;
            
            Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
            bombRigidbody.AddForce(Camera.main.transform.forward * 15f, ForceMode.Impulse);
            bombRigidbody.AddTorque(Vector3.one);
        }
    }
}
