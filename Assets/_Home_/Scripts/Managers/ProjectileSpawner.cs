using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DesignPatterns;

public class ProjectileSpawner : MonoBehaviour
{
    private Pool<Projectile> _projectilePool;
    private Pool<Projectile> projectilePool
    {
        get
        {
            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<ProjectilePool>().projectilePool;
            }
            return _projectilePool;
        }
    }

    [Button]
    public void Spawn()
    {
        //if (projectilePool == null) return;
        Projectile newProjectile = projectilePool.Get();
        newProjectile.transform.position = transform.position;
        newProjectile.transform.rotation = transform.rotation;
        newProjectile.Init(projectilePool);
    }
}