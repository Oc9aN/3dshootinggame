using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Bomb : MonoBehaviour
{
    public static Pool_Bomb Instance;

    [SerializeField]
    private Bomb _bombPrefab;

    [SerializeField]
    private int _poolSize = 3;

    [SerializeField]
    private List<Bomb> _pool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        PoolInitialize();
    }

    private void PoolInitialize()
    {
        _pool = new List<Bomb>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            Bomb bomb = Instantiate(_bombPrefab, transform, true);
            bomb.gameObject.SetActive(false);
            _pool.Add(bomb);
        }
    }

    public Bomb GetBomb()
    {
        foreach (var bomb in _pool)
        {
            if (!bomb.gameObject.activeInHierarchy)
            {
                bomb.gameObject.SetActive(true);
                return bomb;
            }
        }
        return null;
    }
}