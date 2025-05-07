using System.Collections;
using UnityEngine;

public class EnemyDie_Boom : EnemyDie, IExplodable
{
    private const float ATTACK_RANGE = 8f;
    
    private IEnumerator _boomCoroutine;
    
    public EnemyDie_Boom(Enemy enemy) : base(enemy)
    {
    }

    protected override void OnDieHandler()
    {
        base.OnDieHandler();
        if (!ReferenceEquals(_boomCoroutine, null))
        {
            _enemy.StopEnemyStateCoroutine(_boomCoroutine);
        }
        _boomCoroutine = Boom_Coroutine();
        _enemy.StartCoroutine(_boomCoroutine);
    }

    private IEnumerator Boom_Coroutine()
    {
        // 시간이 지난 후 터짐
        yield return new WaitForSeconds(_enemy.Data.DieTime / 2f);

        Explode();
    }

    public void Explode()
    {
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