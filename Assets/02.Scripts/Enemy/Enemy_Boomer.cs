using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boomer : Enemy
{
    protected override void SetEnemyState()
    {
        _enemyStates = new Dictionary<EEnemyState, IEnemyState>()
        {
            { EEnemyState.Idle, new EnemyIdle(this) },
            { EEnemyState.Patrol, new EnemyPatrol(this) },
            { EEnemyState.Trace, new EnemyTrace(this) },
            { EEnemyState.Return, new EnemyReturn(this) },
            { EEnemyState.Attack, new EnemyAttack(this) },
            { EEnemyState.Damaged, new EnemyDamaged(this) },
            { EEnemyState.Die, new EnemyDie(this) }
        };
    }
}