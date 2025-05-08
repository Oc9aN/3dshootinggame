using System.Collections;
using UnityEngine;

public class EnemyDamaged : IEnemyState
{
    private const float FLASH_TIME = 0.1f;
    
    private Enemy _enemy;
    
    private IEnumerator _damagedCoroutine;

    private float _timer = 0f;
    private float _currentKnockBackValue;
    
    private Vector3 _knockBackDirection;
    
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private MaterialPropertyBlock _propertyBlock;
    
    public EnemyDamaged(Enemy enemy)
    {
        _enemy = enemy;
        _skinnedMeshRenderer = _enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void Enter()
    {
        _damagedCoroutine = Damaged_Coroutine();
        _enemy.StartEnemyStateCoroutine(_damagedCoroutine);
        _currentKnockBackValue = _enemy.DamageInfo.KnockBackForce;
        _timer = 0f;
        _knockBackDirection = _enemy.transform.position - _enemy.DamageInfo.From.transform.position;
        _knockBackDirection.y = 0f;
        _knockBackDirection.Normalize();
        _enemy.NavMeshAgent.isStopped = true;
        _enemy.NavMeshAgent.ResetPath();
        
        _enemy.Animator.SetTrigger("Hit");
    }

    public void Acting()
    {
        _timer += Time.deltaTime;
        // 경과 시간 비율 (0 ~ 1)
        float timeRatio = _timer / _enemy.Data.DamagedTime;
        // 감소된 넉백 값 (1에서 0으로 선형 감소)
        _currentKnockBackValue = _enemy.DamageInfo.KnockBackForce * (1f - timeRatio);
        _enemy.CharacterController.Move(_knockBackDirection * (_currentKnockBackValue * Time.deltaTime));
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
        SetEmissionColor(Color.red);
        yield return new WaitForSeconds(FLASH_TIME);
        SetEmissionColor(Color.black);
        yield return new WaitForSeconds(_enemy.Data.DamagedTime - FLASH_TIME);
        _enemy.ChangeState(EEnemyState.Trace);
    }

    private void SetEmissionColor(Color color)
    {
        _skinnedMeshRenderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetColor("_EmissionColor", color);
        _skinnedMeshRenderer.SetPropertyBlock(_propertyBlock);
    }
}