using UnityEngine;

public class EnemyPatrol : IEnemyState
{
    private Enemy _enemy;
    public EnemyPatrol(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Acting()
    {
        // 전이 Trace
        if (Vector3.Distance(_enemy.transform.position, _enemy.Player.transform.position) < _enemy.Data.FindDistance)
        {
            _enemy.ChangeState(EnemyState.Trace);
            return;
        }
        // 전이 Idle
        if (Vector3.Distance(_enemy.transform.position, _enemy.TargetPosition) < _enemy.Data.OnPlaceThreshold)
        {
            _enemy.ChangeState(EnemyState.Idle);
            return;
        }
        // 목표 지점으로 이동
        Vector3 direction = (_enemy.TargetPosition - _enemy.transform.position).normalized;
        _enemy.CharacterController.Move(direction * (_enemy.Data.MoveSpeed * Time.deltaTime));
    }

    public void Exit()
    {
        
    }
}