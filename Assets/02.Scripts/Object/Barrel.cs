using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barrel : MonoBehaviour, IExplodable, IDamageable
{
    // 폭발하는 통
    // TODO: 값이 다른 여러 객체가 필요해지면 SO로 빼기
    [SerializeField]
    private float _minExplodeForce;

    [SerializeField]
    private float _maxExplodeForce;

    [SerializeField]
    private float _explodeRange;

    [SerializeField]
    private Damage _explodeDamage;

    [SerializeField]
    private float _health;
    
    [SerializeField]
    private float _remainingTime;

    [SerializeField]
    private ParticleSystem _explodeVFXPrefab;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Explode()
    {
        StartCoroutine(Explode_Coroutine());
        // 폭발
        ParticleSystem vfx = Instantiate(_explodeVFXPrefab);
        vfx.transform.position = transform.position;
        vfx.Play();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explodeRange);
        foreach (var hitCollider in hitColliders)
        {
            if (ReferenceEquals(hitCollider, gameObject))
            {
                continue;
            }
            Debug.Log(hitCollider.gameObject.name);
            if (hitCollider.TryGetComponent(out IDamageable damageableObject))
            {
                _explodeDamage.From = gameObject;
                // 데미지를 입는 객체인 경우
                damageableObject.TakeDamage(_explodeDamage);
            }
        }

        Vector3 randomDir = Random.onUnitSphere;
        randomDir.y = Mathf.Abs(randomDir.y);
        float randomExplodeForce = Random.Range(_minExplodeForce, _maxExplodeForce);
        _rigidbody.AddForce(randomDir * randomExplodeForce, ForceMode.Impulse);
        _rigidbody.AddTorque(Random.onUnitSphere * randomExplodeForce, ForceMode.Impulse);
    }

    public void TakeDamage(Damage damage)
    {
        if (_health <= 0)
        {
            return;
        }

        _health -= damage.DamageValue;

        if (_health <= 0)
        {
            Debug.Log("폭발");
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);              // 반투명 빨간색
        Gizmos.DrawSphere(transform.position, _explodeRange); // 범위 시각화
    }

    private IEnumerator Explode_Coroutine()
    {
        yield return new WaitForSeconds(_remainingTime);
        Remove();
    }

    private void Remove()
    {
        Destroy(gameObject);
    }
}