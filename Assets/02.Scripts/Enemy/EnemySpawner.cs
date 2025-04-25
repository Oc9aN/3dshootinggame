using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    // 정해진 위치의 랜덤 범위에서 n초마다 생성
    [SerializeField]
    private GameObject _target;
    
    [SerializeField]
    private SO_Enemy _enemyData;
    
    [SerializeField]
    private float _spawnRange = 10f;
    
    [SerializeField]
    private float _spawnDelay = 1f;

    [SerializeField]
    private float _spawnHeight = 1f;
    
    private float _spawnTimer = 0f;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnDelay)
        {
            _spawnTimer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = Pool_Enemy.Instance.GetPooledObject();
        if (ReferenceEquals(enemy, null))
        {
            return;
        }

        Vector3 randomPosition = new Vector3(Random.Range(-_spawnRange, _spawnRange), 0,
            Random.Range(-_spawnRange, _spawnRange));
        Vector3 spawnPosition = transform.position + randomPosition;
        spawnPosition.y = _spawnHeight;
        enemy.transform.position = spawnPosition;
        enemy.Data = _enemyData;
        enemy.Target = _target;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);                                            // 반투명 빨간색
        Gizmos.DrawCube(transform.position, new Vector3(_spawnRange * 2, 1f, _spawnRange * 2)); // 범위 시각화
    }
}
