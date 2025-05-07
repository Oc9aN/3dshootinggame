using System.Collections.Generic;
using UnityEngine;

public class Pool_Bullet : Singleton<Pool_Bullet>
{
    [SerializeField]
    private Bullet _poolObjectPrefab;

    [SerializeField]
    private int _poolSize = 3;
    
    private Pool<Bullet> _pool;
    
    protected override void InternalAwake()
    {
        base.InternalAwake();
        _pool = new Pool<Bullet>(_poolObjectPrefab, _poolSize, gameObject);
    }

    public Bullet GetPooledObject()
    {
        return _pool.GetPooledObject();
    }
    
    public void ReturnPooledObject(Bullet pooledObject)
    {
        _pool.ReturnPooledObject(pooledObject);
    }
}