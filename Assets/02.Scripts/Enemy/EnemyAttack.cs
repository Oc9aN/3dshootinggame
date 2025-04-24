using UnityEngine;

public class EnemyAttack : IEnemyState
{
    private Enemy _enemy;
    
    private float _attackCoolTimer = 0f;
    
    public EnemyAttack(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Acting()
    {
        // 전이 Trace
        if (Vector3.Distance(_enemy.transform.position, _enemy.Player.transform.position) >= _enemy.Data.AttackDistance)
        {
            _attackCoolTimer = 0f;
            _enemy.ChangeState(EnemyState.Trace);
            return;
        }
        // 공격
        _attackCoolTimer += Time.deltaTime;
        if (_attackCoolTimer >= _enemy.Data.AttackCoolTime)
        {
            Debug.Log("Attack");
            _attackCoolTimer = 0f;
        }
    }

    public void Exit()
    {
        
    }
}