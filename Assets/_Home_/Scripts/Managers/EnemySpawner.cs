using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;
using PathCreation;

public class EnemySpawner : MonoBehaviour
{
    public PathCreator pathCreator;
    public Enemy enemyPrefab;
    private Pool<Enemy> enemyPool;
    private void Start()
    {
        enemyPool = new Pool<Enemy>(
            3, 50, prefab: enemyPrefab
        );
    }

    [Button]
    public void Spawn()
    {
        Enemy newEnemy = enemyPool.Get();
        newEnemy.transform.position = transform.position;
        newEnemy.Init(enemyPool, pathCreator);
    }
}
