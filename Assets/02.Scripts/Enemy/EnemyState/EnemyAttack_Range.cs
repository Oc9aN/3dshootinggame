using System.Collections;
using UnityEngine;

public class EnemyAttack_Range : EnemyAttack
{
    private const float RANGE_ATTACK_TIME = 0.3f;
    private const float ATTACK_RANGE = 8f;
    
    public EnemyAttack_Range(Enemy enemy) : base(enemy)
    {
        
    }

    protected override IEnumerator Attack_Coroutine()
    {
        // 공격 방식을 재정의 (범위 공격)
        // 아군도 공격
        yield return new WaitForSeconds(RANGE_ATTACK_TIME);
        
        Collider[] colliders = Physics.OverlapSphere(_enemy.transform.position, ATTACK_RANGE);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_enemy.Data.Damage);
            }
        }
    }
}