using System.Collections.Generic;
using UnityEngine;

public class Pool_BulletEffect : Singleton<Pool_BulletEffect>
{
    [SerializeField]
    private BulletParticle _poolObjectPrefab;

    [SerializeField]
    private int _poolSize = 3;
    
    private Pool<BulletParticle> _pool;
    
    protected override void InternalAwake()
    {
        base.InternalAwake();
        _pool = new Pool<BulletParticle>(_poolObjectPrefab, _poolSize, gameObject);
    }
    
    public BulletParticle GetPooledObject()
    {
        return _pool.GetPooledObject();
    }
    
    public void ReturnPooledObject(BulletParticle pooledObject)
    {
        _pool.ReturnPooledObject(pooledObject);
    }
}