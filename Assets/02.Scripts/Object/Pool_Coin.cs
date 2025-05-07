using System.Collections.Generic;
using UnityEngine;

public class Pool_Coin : Singleton<Pool_Coin>
{
    [SerializeField]
    private Coin _poolObjectPrefab;

    [SerializeField]
    private int _poolSize = 3;
    
    private Pool<Coin> _pool;
    
    protected override void InternalAwake()
    {
        base.InternalAwake();
        _pool = new Pool<Coin>(_poolObjectPrefab, _poolSize, gameObject);
    }
    
    public Coin GetPooledObject()
    {
        return _pool.GetPooledObject();
    }
    
    public void ReturnPooledObject(Coin pooledObject)
    {
        _pool.ReturnPooledObject(pooledObject);
    }
}