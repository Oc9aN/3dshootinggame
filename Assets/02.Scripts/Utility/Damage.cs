using System;
using UnityEngine;

[Serializable]
public struct Damage
{
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

    public void SetFrom(GameObject from)
    {
        _from = from;
    }
}