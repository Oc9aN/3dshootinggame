using System;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoolObject, IExplodable
{
    [SerializeField]
    private GameObject _explosionEffectPrefab;

    [SerializeField]
    private float _explodeRange;
    
    [SerializeField]
    private Damage _explodeDamage ;

    private Rigidbody _rigidbody;
    
    private Camera _camera;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    public void Fire(float force)
    {
        _rigidbody.AddForce(_camera.transform.forward * force, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.one);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    public void Initialize()
    {
        
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explodeRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out IDamageable damageableObject))
            {
                _explodeDamage.From = gameObject;
                // 데미지를 입는 객체인 경우
                damageableObject.TakeDamage(_explodeDamage);
            }
        }
        
        GameObject effectObject = Instantiate(_explosionEffectPrefab);
        effectObject.transform.position = transform.position;
        
        Pool_Bomb.Instance.ReturnPooledObject(this);
    }
}
