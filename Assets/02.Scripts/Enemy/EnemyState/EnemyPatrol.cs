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
        // 목표 위치 설정
        _enemy.TargetPosition = _enemy.PatrolPoints[Random.Range(0, _enemy.PatrolPoints.Count)].position;
    }

    public void Acting()
    {
        // 전이 Trace
        if (Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position) < _enemy.Data.FindDistance)
        {
            _enemy.ChangeState(EEnemyState.Trace);
            return;
        }
        // 전이 Idle
        if (Vector3.Distance(_enemy.transform.position, _enemy.TargetPosition) < _enemy.Data.OnPlaceThreshold)
        {
            _enemy.ChangeState(EEnemyState.Idle);
            return;
        }
        // 목표 지점으로 이동
        _enemy.NavMeshAgent.SetDestination(_enemy.TargetPosition);
        // Vector3 direction = (_enemy.TargetPosition - _enemy.transform.position).normalized;
        // _enemy.CharacterController.Move(direction * (_enemy.Data.MoveSpeed * Time.deltaTime));
    }

    public void Exit()
    {
        
    }
}