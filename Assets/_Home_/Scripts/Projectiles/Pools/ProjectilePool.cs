using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public abstract class ProjectilePool<T> : MonoBehaviour where T : Projectile
{
    public T projectilePrefab;
    public virtual Pool<T> projectilePool
    {
        get;
        protected set;
    }
    protected virtual void OnEnable()
    {
        projectilePool = new Pool<T>(
            3, 50, prefab: projectilePrefab
        );
    }
}
