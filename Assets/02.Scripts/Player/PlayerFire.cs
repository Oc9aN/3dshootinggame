using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 마우스 왼쪽과 오른쪽으로 총알과 수류탄을 발사

    [SerializeField]
    private GameObject _firePosition;

    [SerializeField]
    private GameObject _bombPrefab;
    
    [SerializeField]
    private ParticleSystem _bulletEffect;

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

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _bulletEffect.transform.position = hit.point;
                _bulletEffect.transform.forward = hit.normal;
                _bulletEffect.Play();
            }
        }
    }
}
