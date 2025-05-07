using System.Collections;
using UnityEngine;

public class EnemyAttack_Range : EnemyAttack
{
    private const float ATTACK_TIME = 0.7f;
    private const float ATTACK_RANGE = 8f;
    private const float ATTACKING_TIME = 4f;
    
    public EnemyAttack_Range(Enemy enemy) : base(enemy) { }

    protected override void OnAttack()
    {
        // 공격 방식을 재정의 (범위 공격)
        // 아군도 공격
        Collider[] colliders = Physics.OverlapSphere(_enemy.transform.position, ATTACK_RANGE);
        Debug.Log(colliders.Length);
        foreach (var collider in colliders)
        {
            if (ReferenceEquals(collider.gameObject, _enemy.gameObject))
            {
                continue;
            }
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                Damage damage = _enemy.Data.Damage;
                damage.From = _enemy.gameObject;
                damageable.TakeDamage(damage);
            }
        }
    }
}