using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Pool_Enemy : Singleton<Pool_Enemy>
{
    private const string POOLING_LABEL = "EnemyPoolingObjects";

    [SerializeField]
    private int _poolSize = 3;

    private Dictionary<EEnemyType, Pool<Enemy>> _pool;

    private bool _isInitialized = false; // 초기화 완료 여부 추적

    private AsyncOperationHandle<IList<GameObject>> _loadHandle;

    protected override void Awake()
    {
        base.Awake();
        Singleton<Pool_Enemy>.WhenInstantiated((_) => InitializePool());
    }

    private async void InitializePool()
    {
        await LoadPrefabsAndInitialize();
        _isInitialized = true; // 초기화 완료 표시
    }

    private async Task LoadPrefabsAndInitialize()
    {
        _pool = new Dictionary<EEnemyType, Pool<Enemy>>();

        _loadHandle = Addressables.LoadAssetsAsync<GameObject>(POOLING_LABEL, null);
        await _loadHandle.Task;

        if (_loadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            IList<GameObject> loadedPrefabs = _loadHandle.Result;
            if (loadedPrefabs.Count == (int)EEnemyType.Count)
            {
                for (int i = 0; i < loadedPrefabs.Count; i++)
                {
                    Enemy prefab = loadedPrefabs[i].GetComponent<Enemy>();
                    if (prefab != null)
                    {
                        _pool.Add((EEnemyType)Enum.Parse(typeof(EEnemyType), loadedPrefabs[i].name),
                            new Pool<Enemy>(prefab, _poolSize, gameObject));
                    }
                    else
                    {
                        Debug.LogError($"로드된 에셋 '{loadedPrefabs[i].name}'에 Enemy 컴포넌트가 없습니다.");
                    }
                }
            }
            else
            {
                Debug.LogError(
                    $"라벨 '{POOLING_LABEL}'로 로드된 에셋의 개수가 Enemy 타입 개수와 일치하지 않습니다. 로드된 개수: {loadedPrefabs.Count}, 필요한 개수: {(int)EEnemyType.Count}");
            }
        }
        else
        {
            Debug.LogError($"라벨 '{POOLING_LABEL}'을 가진 에셋 로드 실패: {_loadHandle.OperationException}");
        }
    }

    public Enemy GetPooledObject(EEnemyType type)
    {
        return _isInitialized ? _pool[type].GetPooledObject() : null;
    }

    public void ReturnPooledObject(Enemy pooledObject)
    {
        _pool[pooledObject.Data.EnemyType].ReturnPooledObject(pooledObject);
    }

    private void OnApplicationQuit()
    {
        Addressables.Release(_loadHandle);
    }
}