using UnityEngine;

public class EnemyAttackEvent : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;

    public void AttackEvent()
    {
        //_enemy.
        _enemy.Attack();
    }
}
