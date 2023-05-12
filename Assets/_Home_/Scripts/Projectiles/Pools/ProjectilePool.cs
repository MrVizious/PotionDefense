using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class ProjectilePool : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Pool<Projectile> projectilePool
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

    public Pool<Projectile> CreatePool(Projectile newProjectilePrefab = null)
    {
        if (projectilePool != null)
        {
            Debug.LogError("A pool is already created");
            // TODO: Make the pool change prefab
        }
        if (newProjectilePrefab != null) projectilePrefab = newProjectilePrefab;
        if (projectilePrefab == null) return null;
        projectilePool = new Pool<Projectile>(
            3, 50, prefab: projectilePrefab
        );
        return projectilePool;
    }
    public Projectile Get()
    {
        return projectilePool.Get();
    }
}
