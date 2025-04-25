using System;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤으로 접근 가능한 풀
public class Pool<T> : Singleton<Pool<T>> where T : MonoBehaviour, IPoolObject
{
    [SerializeField]
    protected T _poolObjectPrefab;

    [SerializeField]
    protected int _poolSize = 3;

    [SerializeField]
    protected Stack<T> _pool;

    private void Start()
    {
        PoolInitialize();
    }

    private void PoolInitialize()
    {
        _pool = new Stack<T>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            T poolObject = Instantiate(_poolObjectPrefab, transform, true);
            poolObject.gameObject.SetActive(false);
            _pool.Push(poolObject);
        }
    }

    public T GetPooledObject()
    {
        // 있는 경우 꺼냄
        if (_pool.TryPop(out T pooledObject))
        {
            pooledObject.gameObject.SetActive(true);
            pooledObject.Initialize();
            return pooledObject;
        }
        // 새 오브젝트를 풀링
        pooledObject = Instantiate(_poolObjectPrefab, transform, true);
        return pooledObject;
    }

    public void ReturnPooledObject(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        _pool.Push(pooledObject);
    }
}