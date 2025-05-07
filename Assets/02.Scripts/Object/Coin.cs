using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour, IPoolObject
{
    // 코인 등장 효과 후 플레이어로 이동
    [SerializeField]
    private float _targetDistance;

    [SerializeField]
    private float _bounceForce;

    private Transform _target;

    private IEnumerator _coinCoroutine;
    
    private Sequence _sequence;

    private void Update()
    {
        if (!ReferenceEquals(_target, null))
        {
            if (Vector3.Distance(transform.position, _target.position) < _targetDistance)
            {
                if (!ReferenceEquals(_coinCoroutine, null))
                {
                    return;
                }

                _coinCoroutine = Coin_Coroutine();
                StartCoroutine(_coinCoroutine);
            }
        }
    }
    
    public void Initialize()
    {
        _target = null;
        _coinCoroutine = null;
    }

    public void OnEnableHandler(Transform target)
    {
        _target = target;

        _sequence = DOTween.Sequence();
        float originalHeight = transform.position.y;
        float randomDuration = Random.Range(0.8f, 1.2f);
        _sequence.Append(transform.DOMoveY(originalHeight + _bounceForce, randomDuration).SetEase(Ease.InBounce));
        _sequence.Append(transform.DOMoveY(originalHeight, randomDuration).SetEase(Ease.OutBounce));
        _sequence.SetLoops(-1, LoopType.Restart);
    }

    private IEnumerator Coin_Coroutine()
    {
        float timer = 0f;
        Vector3 startPoint = transform.position;
        Vector3 controlPoint = transform.position + Random.insideUnitSphere * 3f;
        controlPoint.y = Mathf.Abs(controlPoint.y);
        while (timer < 1f)
        {
            timer += Time.deltaTime * 2f;
            transform.position = CalculateBezierPoint(timer, startPoint, controlPoint, _target.position);
            yield return null;
        }

        _sequence.Kill();
        Pool_Coin.Instance.ReturnPooledObject(this);
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // (1 - t)^2 * p0 + 2(1 - t)t * p1 + t^2 * p2
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}