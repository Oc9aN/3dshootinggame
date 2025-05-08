using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
        if (ViewManager.Instance.ViewType == EViewType.FirstPerson ||
            ViewManager.Instance.ViewType == EViewType.ThirdPerson)
        {
            _rigidbody.AddForce(_camera.transform.forward * force, ForceMode.Impulse);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float targetY = transform.position.y;
            Vector3 targetPoint = ray.GetPoint((targetY - ray.origin.y) / ray.direction.y);
            Vector3 aimDirection = targetPoint - transform.position;
            _rigidbody.AddForce(aimDirection.normalized * force, ForceMode.Impulse);
        }
        _rigidbody.AddTorque(Random.insideUnitSphere);
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
            Debug.Log(hitCollider.gameObject.name);
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
