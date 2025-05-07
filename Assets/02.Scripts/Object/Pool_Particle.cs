using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Particle : Singleton<Pool_Particle>
{
    // 파티클을 위한 풀
    [Serializable]
    public struct ParticlePrefabInfo
    {
        [SerializeField]
        private EParticleType _type;
        public EParticleType Type => _type;

        [SerializeField]
        private ParticleSystem _prefab;
        public ParticleSystem Prefab => _prefab;

        [SerializeField]
        private int _initialPoolSize;
        public int InitialPoolSize => _initialPoolSize;
    }

    [SerializeField]
    private List<ParticlePrefabInfo> _particlePrefabs;

    private Dictionary<EParticleType, Stack<ParticleSystem>> _pool;

    private List<GameObject> _poolParents;

    protected override void InternalAwake()
    {
        base.InternalAwake();
        _pool = new Dictionary<EParticleType, Stack<ParticleSystem>>(_particlePrefabs.Count);
        _poolParents = new List<GameObject>(_particlePrefabs.Count);
        for (int i = 0; i < _particlePrefabs.Count; i++)
        {
            GameObject parent = new GameObject($"{_particlePrefabs[i].Prefab.name}_Pool");
            parent.transform.SetParent(transform);
            _poolParents.Add(parent);
            _pool.Add(_particlePrefabs[i].Type, new Stack<ParticleSystem>(_particlePrefabs[i].InitialPoolSize));
            for (int j = 0; j < _particlePrefabs[i].InitialPoolSize; j++)
            {
                ParticleSystem particle = Instantiate(_particlePrefabs[i].Prefab, parent.transform);
                particle.gameObject.SetActive(false);
                _pool[_particlePrefabs[i].Type].Push(particle);
            }
        }
    }

    public ParticleSystem GetPooledObject(EParticleType type)
    {
        // 있는 경우 꺼냄
        if (_pool[type].TryPop(out ParticleSystem pooledObject))
        {
            pooledObject.Stop();
            pooledObject.gameObject.SetActive(true);
            return pooledObject;
        }

        // 새 오브젝트를 풀링
        pooledObject = Instantiate(_particlePrefabs[(int)type].Prefab, _poolParents[(int)type].transform);
        return pooledObject;
    }

    public void ReturnPooledObject(ParticleSystem pooledObject, EParticleType type)
    {
        pooledObject.gameObject.SetActive(false);
        _pool[type].Push(pooledObject);
    }
}