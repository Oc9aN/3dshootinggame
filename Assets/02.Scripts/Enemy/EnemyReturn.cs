using UnityEngine;

public class EnemyReturn : IEnemyState
{
    private Enemy _enemy;
    public EnemyReturn(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Acting()
    {
        // 전이 Idle
        if (Vector3.Distance(_enemy.transform.position, _enemy.TargetPosition) < _enemy.Data.OnPlaceThreshold)
        {
            _enemy.ChangeState(EnemyState.Idle);
            return;
        }
        // 전이 Trace
        if (Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position) < _enemy.Data.FindDistance)
        {
            _enemy.ChangeState(EnemyState.Trace);
            return;
        }
        
        // 제자리로 복귀
        _enemy.NavMeshAgent.SetDestination(_enemy.TargetPosition);
        // Vector3 direction = (_enemy.TargetPosition - _enemy.transform.position).normalized;
        // _enemy.CharacterController.Move(direction * (_enemy.Data.MoveSpeed * Time.deltaTime));
    }

    public void Exit()
    {
        
    }
}