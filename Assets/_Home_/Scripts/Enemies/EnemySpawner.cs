using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;
using PathCreation;

public class EnemySpawner : MonoBehaviour
{
    public PathCreator pathCreator;
    public Wave spawnSequence;
    private int currentActionIndex = 0;
    private Dictionary<Enemy, EnemyPool> enemyPools = new Dictionary<Enemy, EnemyPool>();


    private void Start()
    {
        currentActionIndex = 0;
        ExecuteCurrentAction();
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
    }

    private void ExecuteCurrentAction()
    {
        if (currentActionIndex >= spawnSequence.spawnerActions.Count) return;
        spawnSequence.spawnerActions[currentActionIndex].onEnd += ExecuteNextAction;
        if (spawnSequence.spawnerActions[currentActionIndex] is WaveActionSpawn)
        {
            ((WaveActionSpawn)spawnSequence.spawnerActions[currentActionIndex]).spawner = this;
        }
        spawnSequence.spawnerActions[currentActionIndex].Begin();
    }

    private void ExecuteNextAction()
    {
        spawnSequence.spawnerActions[currentActionIndex].onEnd -= ExecuteNextAction;
        currentActionIndex++;
        ExecuteCurrentAction();
    }
}