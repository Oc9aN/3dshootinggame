using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionEffectPrefab;

    private Rigidbody _rigidbody;
    
    private Camera _camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    public void Fire()
    {
        _rigidbody.AddForce(_camera.transform.forward * 15f, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.one);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effectObject = Instantiate(_explosionEffectPrefab);
        effectObject.transform.position = transform.position;
        
        Destroy(gameObject);
    }
}
