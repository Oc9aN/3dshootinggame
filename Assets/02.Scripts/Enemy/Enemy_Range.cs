using System.Collections.Generic;

public class Enemy_Range : Enemy
{
    protected override void SetEnemyState()
    {
        _enemyStates = new Dictionary<EEnemyState, IEnemyState>()
        {
            { EEnemyState.Idle, new EnemyIdle(this) },
            { EEnemyState.Patrol, new EnemyPatrol(this) },
            { EEnemyState.Trace, new EnemyTrace(this) },
            { EEnemyState.Return, new EnemyReturn(this) },
            { EEnemyState.Attack, new EnemyAttack_Range(this) },
            { EEnemyState.Damaged, new EnemyDamaged(this) },
            { EEnemyState.Die, new EnemyDie(this) }
        };
    }
}