using System.Collections.Generic;
using UnityEngine;

public class Pool_Bomb : Singleton<Pool_Bomb>
{
    [SerializeField]
    private Bomb _poolObjectPrefab;

    [SerializeField]
    private int _poolSize = 3;
    
    private Pool<Bomb> _pool;
    
    protected override void InternalAwake()
    {
        base.InternalAwake();
        _pool = new Pool<Bomb>(_poolObjectPrefab, _poolSize, gameObject);
    }

    public Bomb GetPooledObject()
    {
        return _pool.GetPooledObject();
    }
    
    public void ReturnPooledObject(Bomb pooledObject)
    {
        _pool.ReturnPooledObject(pooledObject);
    }
}