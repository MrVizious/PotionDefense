using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DesignPatterns;

public class FollowingProjectileSpawner : MonoBehaviour
{
    protected Pool<FollowingProjectile> _projectilePool;
    protected Pool<FollowingProjectile> projectilePool
    {
        get
        {
            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<FollowingProjectilePool>().projectilePool;
            }
            return _projectilePool;
        }
    }

    [Button]
    public void Spawn()
    {
        //if (projectilePool == null) return;
        FollowingProjectile newProjectile = projectilePool.Get();
        newProjectile.transform.position = transform.position;
        newProjectile.transform.rotation = transform.rotation;
        newProjectile.Init(projectilePool);
    }
}