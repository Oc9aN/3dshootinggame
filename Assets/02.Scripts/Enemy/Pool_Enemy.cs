using System.Collections.Generic;
using UnityEngine;

public class Pool_Enemy : Singleton<Pool_Enemy>
{
    [SerializeField]
    private List<Enemy> _poolObjectPrefab;

    [SerializeField]
    private int _poolSize = 3;
    
    private Dictionary<EEnemyType, Pool<Enemy>> _pool;

    protected override void InternalAwake()
    {
        base.InternalAwake();
        _pool = new Dictionary<EEnemyType, Pool<Enemy>>();
        for (int i = 0; i < (int)EEnemyType.Count; i++)
        {
            _pool.Add((EEnemyType)i, new Pool<Enemy>(_poolObjectPrefab[i],  _poolSize, gameObject));
        }
    }

    public Enemy GetPooledObject(EEnemyType type)
    {
        return _pool[type].GetPooledObject();
    }

    public void ReturnPooledObject(Enemy pooledObject)
    {
        _pool[pooledObject.Data.EnemyType].ReturnPooledObject(pooledObject);
    }
}