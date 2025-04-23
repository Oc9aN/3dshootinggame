using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : Singleton<Pool<T>> where T : MonoBehaviour
{
    [SerializeField]
    protected T _poolObjectPrefab;

    [SerializeField]
    protected int _poolSize = 3;

    [SerializeField]
    protected List<T> _pool;

    private void Start()
    {
        PoolInitialize();
    }

    private void PoolInitialize()
    {
        _pool = new List<T>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            T poolObject = Instantiate(_poolObjectPrefab, transform, true);
            poolObject.gameObject.SetActive(false);
            _pool.Add(poolObject);
        }
    }

    public T GetPooledObject()
    {
        foreach (var pooledObject in _pool)
        {
            if (!pooledObject.gameObject.activeInHierarchy)
            {
                pooledObject.gameObject.SetActive(true);
                return pooledObject;
            }
        }
        return null;
    }
}