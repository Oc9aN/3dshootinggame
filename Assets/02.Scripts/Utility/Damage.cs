using UnityEngine;

public struct Damage
{
    private int _value;
    public int Value => _value;
    private GameObject _from;
    public GameObject From => _from;

    public Damage(int value, GameObject from)
    {
        _value = value;
        _from = from;
    }
}