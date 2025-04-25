using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    private Vector3 _hitPoint;
    private Vector3 _startPoint;
    private float _distance;
    private float _remainingDistance;
    private float _speed;
    private bool _isFired = false;
    
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
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
                _isFired = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void Fire(Vector3 hitPoint, float speed)
    {
        _trailRenderer.Clear();
        _isFired = true;
        
        _startPoint = transform.position;
        _hitPoint = hitPoint;
        _distance = Vector3.Distance(transform.position, _hitPoint);
        _remainingDistance = _distance;
        _speed = speed;
    }

    public void Initialize()
    {
        
    }
}