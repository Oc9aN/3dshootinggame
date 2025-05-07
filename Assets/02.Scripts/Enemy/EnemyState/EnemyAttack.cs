using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : IEnemyState
{
    private const float ATTACK_TIME = 0.3f;
    private const float ATTACKING_TIME = 1f;

    protected Enemy _enemy;

    private float _attackCoolTimer = 0f;
    
    protected bool _isAttacking;

    public EnemyAttack(Enemy enemy)
    {
        _enemy = enemy;

        _enemy.OnAttack += OnAttack;
        _enemy.OnAttackEnd += () => _isAttacking = false;
    }

    public void Enter()
    {
        _enemy.NavMeshAgent.velocity = Vector3.zero;
        _enemy.NavMeshAgent.isStopped = true;
        _enemy.NavMeshAgent.ResetPath();

        // 첫 공격은 빠르게
        _attackCoolTimer = _enemy.Data.AttackCoolTime / 2f;
    }

    public void Acting()
    {
        // 전이 Trace
        if (!_isAttacking && Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position) >= _enemy.Data.AttackDistance)
        {
            _attackCoolTimer = 0f;
            _enemy.ChangeState(EEnemyState.Trace);

            _enemy.Animator.SetTrigger("AttackDelayToMove");
            return;
        }

        AttackRoutine();
    }

    public void Exit()
    {

    }

    private void AttackRoutine()
    {
        // 공격
        _attackCoolTimer += Time.deltaTime;
        if (_attackCoolTimer >= _enemy.Data.AttackCoolTime)
        {
            Debug.Log("Attack");
            _enemy.Animator.SetTrigger("AttackDelayToAttack");

            _attackCoolTimer = 0f;
            
            _isAttacking = true;
        }
    }

    protected virtual void OnAttack()
    {
        _enemy.Target.GetComponent<IDamageable>().TakeDamage(_enemy.Data.Damage);
    }
}