using System.Collections.Generic;
using UnityEngine;

public class Pool_Bullet : Singleton<Pool_Bullet>
{
    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private int _poolSize = 100;

    [SerializeField]
    private List<Bullet> _pool;

    private void Start()
    {
        PoolInitialize();
    }

    private void PoolInitialize()
    {
        _pool = new List<Bullet>(_poolSize);
        for (int i = 0; i < _poolSize; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab, transform, true);
            bullet.gameObject.SetActive(false);
            _pool.Add(bullet);
        }
    }

    public Bullet GetBullet()
    {
        foreach (var bullet in _pool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }
        return null;
    }
}
