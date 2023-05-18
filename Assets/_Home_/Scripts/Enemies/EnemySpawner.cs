using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;
using PathCreation;
using UltEvents;
using Cysharp.Threading.Tasks;

public class EnemySpawner : MonoBehaviour
{
    public LevelData levelData;
    public PathCreator pathCreator;
    public UltEvent onWaveEnded = new UltEvent();
    public int currentWaveIndex = 0;
    private int currentActionIndex = 0;
    private Wave currentWave
    {
        get => levelData.waves[currentWaveIndex];
    }
    private Dictionary<Enemy, EnemyPool> enemyPools = new Dictionary<Enemy, EnemyPool>();

    private int spawnedEnemiesCounter = 0;


    private void Start()
    {
        currentWaveIndex = 0;
        spawnedEnemiesCounter = 0;
    }

    public void ExecuteWave(int newWaveIndex)
    {
        currentWaveIndex = newWaveIndex;
        spawnedEnemiesCounter = 0;
        currentActionIndex = 0;
        WaveEndedChecker().Forget();
        ExecuteCurrentAction();
    }

    private async UniTask WaveEndedChecker()
    {
        await UniTask.WaitUntil(() => currentActionIndex >= currentWave.waveActions.Count && spawnedEnemiesCounter <= 0);
        onWaveEnded.Invoke();
    }

    private EnemyPool GetEnemyPool(Enemy prefab)
    {
        foreach (EnemyPool pool in FindObjectsByType<EnemyPool>(FindObjectsSortMode.None))
        {
            if (pool.enemyPrefab == prefab)
            {
                return pool;
            }
        }
        EnemyPool newEnemyPool = new GameObject("Enemy Pool " + prefab.name)
                                            .AddComponent<EnemyPool>();
        newEnemyPool.enemyPrefab = prefab;
        newEnemyPool.CreatePool();
        return newEnemyPool;
    }

    [Button]
    public void Spawn(Enemy prefab)
    {
        EnemyPool enemyPool = null;
        enemyPools.TryGetValue(prefab, out enemyPool);
        if (enemyPool == null)
        {
            enemyPool = GetEnemyPool(prefab);
            enemyPools.Add(prefab, enemyPool);
        }
        Enemy newEnemy = enemyPool.Get();
        newEnemy.transform.position = transform.position;
        newEnemy.path = pathCreator;

        spawnedEnemiesCounter++;

        newEnemy.onRelease += () => spawnedEnemiesCounter--;
    }

    private void ExecuteCurrentAction()
    {
        if (currentActionIndex >= currentWave.waveActions.Count) return;
        currentWave.waveActions[currentActionIndex].onEnd += ExecuteNextAction;
        currentWave.waveActions[currentActionIndex].Begin(this);
    }

    private void ExecuteNextAction()
    {
        currentWave.waveActions[currentActionIndex].onEnd -= ExecuteNextAction;
        currentActionIndex++;
        ExecuteCurrentAction();
    }
}