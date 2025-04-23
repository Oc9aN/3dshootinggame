using System;
using UnityEngine;

[Serializable]
public struct Damage
{
    // TODO: From의 경우 DamageWapper클래스로 분리?
    [SerializeField]
    private int _damageValue;
    public int DamageValue => _damageValue;
    
    [SerializeField]
    private float _knockBackForce;
    public float KnockBackForce => _knockBackForce;
    
    private GameObject _from;
    public GameObject From => _from;

    public Damage(int damageValue, float knockBackForce, GameObject from)
    {
        _damageValue = damageValue;
        _knockBackForce = knockBackForce;
        _from = from;
    }
    
    public Damage(GameObject from, Damage damage)
    {
        _damageValue = damage.DamageValue;
        _knockBackForce = damage.DamageValue;
        _from = from;
    }
}