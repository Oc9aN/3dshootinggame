using System;
using UnityEngine;

public class EnemyPresenter : MonoBehaviour
{
    private Enemy _enemy;
    private UI_EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyHealth = GetComponentInChildren<UI_EnemyHealth>();
    }

    private void Start()
    {
        _enemy.OnDmaged += _enemyHealth.RefreshSliderValue;
        
        _enemyHealth.Initialize(_enemy.Data.MaxHealth);
    }
}
