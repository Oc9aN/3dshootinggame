using UnityEngine;

public class Barrel : MonoBehaviour, IExplodable, IDamageable
{
    // 폭발하는 통
    // TODO: 값이 다른 여러 객체가 필요해지면 SO로 빼기
    [SerializeField]
    private float _explodeRange;
    
    [SerializeField]
    private Damage _explodeDamage;
    
    private float _health;

    public void Explode()
    {
        // 폭발
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
    }

    public void TakeDamage(Damage damage)
    {
        _health -= damage.DamageValue;

        if (_health <= 0)
        {
            Explode();
        }
    }
}