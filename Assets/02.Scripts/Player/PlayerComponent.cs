using System;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    private Player _player;
    
    public Player Player => _player;

    protected virtual void Awake()
    {
        _player = GetComponent<Player>();
    }
}
