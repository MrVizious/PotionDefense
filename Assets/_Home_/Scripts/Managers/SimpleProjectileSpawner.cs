using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DesignPatterns;

public class SimpleProjectileSpawner : MonoBehaviour
{
    protected Pool<SimpleProjectile> _projectilePool;
    protected Pool<SimpleProjectile> projectilePool
    {
        get
        {
            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<SimpleProjectilePool>().projectilePool;
            }
            return _projectilePool;
        }
    }

    [Button]
    public void Spawn()
    {
        //if (projectilePool == null) return;
        SimpleProjectile newProjectile = projectilePool.Get();
        newProjectile.transform.position = transform.position;
        newProjectile.transform.rotation = transform.rotation;
        newProjectile.Init(projectilePool);
    }
}