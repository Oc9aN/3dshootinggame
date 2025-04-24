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

    private void Start()
    {
        StartCoroutine(Spawn_Coroutine());
    }

    private IEnumerator Spawn_Coroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);
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
        Vector3 spawnPosition = new Vector3(Random.Range(-_spawnRange, _spawnRange), 1f, Random.Range(-_spawnRange, _spawnRange));
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
        Gizmos.color = new Color(1, 0, 0, 0.3f);            // 반투명 빨간색
        Gizmos.DrawSphere(transform.position, _spawnRange); // 범위 시각화
    }
}
