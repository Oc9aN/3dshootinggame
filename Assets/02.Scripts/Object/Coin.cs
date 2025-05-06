using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour, IPoolObject
{
    // 코인 등장 효과 후 플레이어로 이동
    [SerializeField]
    private float _bounceForce;

    private Transform _target;
    
    private IEnumerator _coinCoroutine;
    
    public void Initialize()
    {
        
    }

    public void OnEnableHandler(Transform target)
    {
        _target = target;
        
        Sequence seq = DOTween.Sequence();
        float originalHeight = transform.position.y;
        seq.Append(transform.DOMoveY(originalHeight + _bounceForce, 1f).SetEase(Ease.InBounce));
        seq.Append(transform.DOMoveY(originalHeight, 1f).SetEase(Ease.OutBounce));
        seq.OnComplete(() =>
        {
            // 완료 후 흡수
            if (!ReferenceEquals(_coinCoroutine, null))
            {
                StopCoroutine(_coinCoroutine);
            }
            _coinCoroutine = Coin_Coroutine();
            StartCoroutine(_coinCoroutine);
        });
    }

    private IEnumerator Coin_Coroutine()
    {
        float timer = 0f;
        Vector3 startPoint = transform.position;
        Vector3 controlPoint =  transform.position + Random.insideUnitSphere;
        controlPoint.y = Mathf.Abs(controlPoint.y);
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            transform.position = CalculateBezierPoint(timer, startPoint, controlPoint, _target.position);
            yield return null;
        }
        Pool_Coin.Instance.ReturnPooledObject(this);
    }
    
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // (1 - t)^2 * p0 + 2(1 - t)t * p1 + t^2 * p2
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}
