using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// 싱글톤으로 접근 가능한 풀
public class Pool<T> where T : MonoBehaviour, IPoolObject
{
    private T _poolObjectPrefab;

    private int _poolSize = 3;
    
    private GameObject _parent;

    private Stack<T> _pool;
    
    public Pool(T poolObjectPrefab, int poolSize, GameObject parent)
    {
        _poolObjectPrefab =  poolObjectPrefab;
        _poolSize = poolSize;
        _parent = parent;
        PoolInitialize();
    }

    private void PoolInitialize()
    {
        _pool = new Stack<T>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            T poolObject = Object.Instantiate(_poolObjectPrefab, _parent.transform, true);
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
        pooledObject = Object.Instantiate(_poolObjectPrefab, _parent.transform, true);
        return pooledObject;
    }

    public void ReturnPooledObject(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        _pool.Push(pooledObject);
    }
}