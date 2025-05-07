using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : IEnemyState
{
    private const float DEFAULT_ATTACK_TIME = 0.3f;

    protected Enemy _enemy;

    private float _attackCoolTimer = 0f;

    private IEnumerator _attackCoroutine;

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
        if (!ReferenceEquals(_attackCoroutine, null))
        {
            _enemy.StopEnemyStateCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
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

            _attackCoroutine = Attack_Coroutine();
            _enemy.StartEnemyStateCoroutine(_attackCoroutine);
        }
    }

    protected virtual IEnumerator Attack_Coroutine()
    {
        yield return new WaitForSeconds(DEFAULT_ATTACK_TIME);
        _enemy.Target.GetComponent<IDamageable>().TakeDamage(_enemy.Data.Damage);
    }
}