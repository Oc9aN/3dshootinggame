using System.Collections;
using UnityEngine;

public class EnemyDie : IEnemyState
{
    private Enemy _enemy;
    
    private IEnumerator _dieCoroutine;
    
    public EnemyDie(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _dieCoroutine = Die_Coroutine();
        _enemy.StartEnemyStateCoroutine(_dieCoroutine);
        
        _enemy.Animator.SetTrigger("Die");
    }

    public void Acting()
    {
        
    }

    public void Exit()
    {
        _enemy.StopEnemyStateCoroutine(_dieCoroutine);
        _dieCoroutine = null;
    }
    
    private IEnumerator Die_Coroutine()
    {
        // 사망
        yield return new WaitForSeconds(_enemy.Data.DieTime);
        Pool_Enemy.Instance.ReturnPooledObject(_enemy);
    }

    private void OnDieHandler()
    {
        // 코인 드랍
    }
}