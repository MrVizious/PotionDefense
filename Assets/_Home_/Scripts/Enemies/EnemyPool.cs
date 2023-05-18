using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class EnemyPool : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Pool<Enemy> enemyPool
    {
        get;
        protected set;
    }
    private Transform _playerTransform;
    public Transform playerTransform
    {
        get
        {
            if (_playerTransform == null) _playerTransform = FindObjectOfType<PlayerController>().transform;
            return _playerTransform;
        }
    }
    protected void OnEnable()
    {
        CreatePool();
    }

    public Pool<Enemy> CreatePool(Enemy newEnemyPrefab = null)
    {
        if (enemyPool != null)
        {
            Debug.LogError("A pool is already created");
            // TODO: Make the pool change prefab
        }
        if (newEnemyPrefab != null) enemyPrefab = newEnemyPrefab;
        if (enemyPrefab == null) return null;
        enemyPool = new Pool<Enemy>(
            3, 50, prefab: enemyPrefab
        );
        return enemyPool;
    }
    public Enemy Get()
    {
        Enemy enemy = enemyPool.Get();
        enemy.transform.SetParent(transform);
        return enemy;
    }
}
