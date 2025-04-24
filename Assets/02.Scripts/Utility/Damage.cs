using System;
using UnityEngine;

[Serializable]
public class Damage
{
    [SerializeField]
    private int _damageValue;
    public int DamageValue => _damageValue;

    [SerializeField]
    private float _knockBackForce;
    public float KnockBackForce => _knockBackForce;

    private GameObject _from;
    public GameObject From { get => _from; set => _from = value; }
}