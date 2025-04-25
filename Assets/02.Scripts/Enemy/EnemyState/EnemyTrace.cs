using UnityEngine;

public class EnemyTrace : IEnemyState
{
    private Enemy _enemy;
    public EnemyTrace(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Acting()
    {
        // 전이 Return
        if (Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position) >= _enemy.Data.FindDistance)
        {
            _enemy.ChangeState(EEnemyState.Return);
            return;
        }
        // 전이 Attack
        if (Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position) < _enemy.Data.AttackDistance)
        {
            _enemy.ChangeState(EEnemyState.Attack);
            return;
        }
        // 플레이어 추적
        _enemy.NavMeshAgent.SetDestination(_enemy.Target.transform.position);
        //Vector3 direction = (_enemy.Player.transform.position - _enemy.transform.position).normalized;
        //_enemy.CharacterController.Move(direction * (_enemy.Data.MoveSpeed * Time.deltaTime));
    }

    public void Exit()
    {
        
    }
}