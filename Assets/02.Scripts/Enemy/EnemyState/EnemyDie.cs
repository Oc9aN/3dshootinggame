using System.Collections;
using UnityEngine;

public class EnemyDie : IEnemyState
{
    protected Enemy _enemy;
    
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
        
        _enemy.CharacterController.enabled = false;
        
        OnDieHandler();
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

    protected virtual void OnDieHandler()
    {
        // 코인 드랍
        int randomNumber = Random.Range(1, 5);
        for (int i = 0; i < randomNumber; i++)
        {
            Coin coin = Pool_Coin.Instance.GetPooledObject();
            Vector2 randomPosition = Random.insideUnitCircle;
            Vector3 coinPosition = _enemy.transform.position;
            coinPosition.x += randomPosition.x;
            coinPosition.z += randomPosition.y;
            coin.transform.position = coinPosition;
            coin.OnEnableHandler(_enemy.Target.transform);
        }
    }
}