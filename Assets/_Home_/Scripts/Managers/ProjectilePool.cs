using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class ProjectilePool<T> : MonoBehaviour where T : Poolable, IProjectile
{
    public T projectilePrefab;
    public Pool<T> projectilePool
    {
        get;
        private set;
    }
    private void OnEnable()
    {
        projectilePool = new Pool<T>(
            3, 50, prefab: projectilePrefab
        );
    }
}
