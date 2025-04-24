using System.Collections;
using UnityEngine;

public class EnemyDamaged : IEnemyState
{
    private Enemy _enemy;
    
    private IEnumerator _damagedCoroutine;

    private float _timer = 0f;
    private float _currentKnockBackValue;
    
    public EnemyDamaged(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _damagedCoroutine = Damaged_Coroutine();
        _enemy.StartEnemyStateCoroutine(_damagedCoroutine);
        _currentKnockBackValue = _enemy.KnockBackForce;
        _timer = 0f;
    }

    public void Acting()
    {
        _timer += Time.deltaTime;
        // 경과 시간 비율 (0 ~ 1)
        float timeRatio = _timer / _enemy.Data.DamagedTime;
        // 감소된 넉백 값 (1에서 0으로 선형 감소)
        _currentKnockBackValue = _enemy.KnockBackForce * (_enemy.Data.DamagedTime - timeRatio);
        Vector3 direction = (_enemy.transform.position - _enemy.Player.transform.position).normalized;
        _enemy.CharacterController.Move(direction * (_currentKnockBackValue * Time.deltaTime));
    }

    public void Exit()
    {
        if (!ReferenceEquals(_damagedCoroutine, null))
        {
            _enemy.StopEnemyStateCoroutine(_damagedCoroutine);
            _damagedCoroutine = null;
        }
    }

    private IEnumerator Damaged_Coroutine()
    {
        yield return new WaitForSeconds(_enemy.Data.DamagedTime);
        _enemy.ChangeState(EnemyState.Trace);
    }
}