using System;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    public void Attack()
    {
        _enemy.Attack();
    }
}