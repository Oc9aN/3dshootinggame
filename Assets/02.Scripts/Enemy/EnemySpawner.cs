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
    private float _spawnRange = 10f;
    
    [SerializeField]
    private float _spawnDelay = 1f;

    [SerializeField]
    private float _spawnHeight = 1f;
    
    private float _spawnTimer = 0f;

    private void Update()
    {
        Spawning();
    }

    private void Spawning()
    {
        if (GameManager.Instance.GameState != EGameState.Run)
        {
            return;
        }
        
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnDelay)
        {
            _spawnTimer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        Enemy enemy = Pool_Enemy.Instance.GetPooledObject(GetRandomType());
        if (ReferenceEquals(enemy, null))
        {
            Debug.LogWarning("없음");
            return;
        }

        // 모든 적에 대해서 공통적인 초기화 작업
        Vector3 randomPosition = new Vector3(Random.Range(-_spawnRange, _spawnRange), 0,
            Random.Range(-_spawnRange, _spawnRange));
        Physics.Raycast(transform.position + randomPosition,  Vector3.down, out RaycastHit hit, 10f);
        Vector3 spawnPosition = hit.point;
        spawnPosition.y += _spawnHeight;
        enemy.NavMeshAgent.Warp(spawnPosition);
        enemy.Target = _target;
    }

    private EEnemyType GetRandomType()
    {
        int range = Random.Range(0, 100);
        if (range < 25)
        {
            return EEnemyType.Enemy_Range;
        }
        if (range < 50)
        {
            return EEnemyType.Enemy_Boomer;
        }
        return EEnemyType.Enemy_Normal;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);                                            
        Gizmos.DrawCube(transform.position, new Vector3(_spawnRange * 2, 1f, _spawnRange * 2)); // 범위 시각화
    }
}
