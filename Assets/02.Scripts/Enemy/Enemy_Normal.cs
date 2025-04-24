using System.Collections.Generic;

public class Enemy_Normal : Enemy
{
    protected override void SetStates()
    {
        _enemyStates = new Dictionary<EnemyState, IEnemyState>()
        {
            { EnemyState.Idle, new EnemyIdle(this) },
            { EnemyState.Patrol, new EnemyPatrol(this) },
            { EnemyState.Trace, new EnemyTrace(this) },
            { EnemyState.Return, new EnemyReturn(this) },
            { EnemyState.Attack, new EnemyAttack(this) },
            { EnemyState.Damaged, new EnemyDamaged(this) },
            { EnemyState.Die, new EnemyDie(this) }
        };
    }
}