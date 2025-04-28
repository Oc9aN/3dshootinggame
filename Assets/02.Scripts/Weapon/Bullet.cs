using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    [SerializeField]
    private ParticleSystem _bulletEffectPrefab;
    
    private Vector3 _hitPoint;
    private Vector3 _hitNormal;
    private Vector3 _startPoint;
    private float _distance;
    private float _remainingDistance;
    private float _speed;
    private bool _isFired = false;

    private TrailRenderer _trailRenderer;
    
    private ParticleSystem _bulletEffect;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _bulletEffect = Instantiate(_bulletEffectPrefab);
    }

    private void Update()
    {
        if (_isFired)
        {
            // 앞으로 빠르게 나아간다.
            transform.position = Vector3.Lerp(_startPoint, _hitPoint, 1 - (_remainingDistance / _distance));

            _remainingDistance -= _speed * Time.deltaTime;

            if (_remainingDistance <= 0)
            {
                // 충돌
                _bulletEffect.transform.position = _hitPoint;
                _bulletEffect.transform.forward = _hitNormal;
                _bulletEffect.Play();
                _isFired = false;
                Pool_Bullet.Instance.ReturnPooledObject(this);
            }
        }
    }

    public void Fire(Vector3 hitPoint, float speed, Vector3 hitNormal)
    {
        _trailRenderer.Clear();
        _isFired = true;

        _startPoint = transform.position;
        _hitNormal = hitNormal;
        _hitPoint = hitPoint;
        _distance = Vector3.Distance(transform.position, _hitPoint);
        _remainingDistance = _distance;
        _speed = speed;
    }

    public void Initialize()
    {
        _hitPoint = Vector3.zero;
        _hitNormal = Vector3.zero;
        _startPoint = Vector3.zero;
        _distance = 0;
        _remainingDistance = 0;
        _speed = 0;
        _isFired = false;
        _trailRenderer.Clear();
    }
}