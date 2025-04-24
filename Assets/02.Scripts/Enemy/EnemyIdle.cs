using UnityEngine;

public class EnemyIdle : IEnemyState
{
    private Enemy _enemy;
    
    private float _patrolTimer = 0f;
    
    public EnemyIdle(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    // 대기 상태
    public void Enter()
    {
        
    }

    public void Acting()
    {
        // 대기 상태
        // 전이 Trace
        if (Vector3.Distance(_enemy.transform.position, _enemy.Player.transform.position) < _enemy.Data.FindDistance)
        {
            _patrolTimer = 0f;
            _enemy.ChangeState(EnemyState.Trace);
            return;
        }
        
        _patrolTimer += Time.deltaTime;
        if (_patrolTimer >= _enemy.Data.PatrolTime)
        {
            // 순찰
            _patrolTimer = 0f;
            _enemy.ChangeState(EnemyState.Patrol);
        }
    }

    public void Exit()
    {
    }
}