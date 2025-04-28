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
        _enemy.NavMeshAgent.velocity = Vector3.zero;
        _enemy.NavMeshAgent.isStopped = true;
        _enemy.NavMeshAgent.ResetPath();
    }

    public void Acting()
    {
        // 전이 Trace
        if (Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position) >= _enemy.Data.AttackDistance)
        {
            _attackCoolTimer = 0f;
            _enemy.ChangeState(EEnemyState.Trace);
            
            _enemy.Animator.SetTrigger("AttackDelayToMove");
            return;
        }

        Attack();
    }

    public void Exit()
    {
        
    }

    private void Attack()
    {
        // 공격
        _attackCoolTimer += Time.deltaTime;
        if (_attackCoolTimer >= _enemy.Data.AttackCoolTime)
        {
            Debug.Log("Attack");
            _enemy.Animator.SetTrigger("AttackDelayToAttack");
            
            _attackCoolTimer = 0f;
        }
    }
}