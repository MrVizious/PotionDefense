using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;

public class ProjectileSpawner : MonoBehaviour
{
    public bool targetPlayer = false;
    public Projectile prefab;
    protected ProjectilePool _projectilePool;
    protected ProjectilePool projectilePool
    {
        get
        {
            if (_projectilePool == null)
            {
                foreach (ProjectilePool pool in FindObjectsByType<ProjectilePool>(FindObjectsSortMode.None))
                {
                    if (pool.projectilePrefab == prefab)
                    {
                        _projectilePool = pool;
                        break;
                    }
                }
                if (_projectilePool == null)
                {
                    ProjectilePool newProjectilePool = new GameObject("Projectile Pool " + prefab.name)
                                                        .AddComponent<ProjectilePool>();
                    newProjectilePool.projectilePrefab = prefab;
                    newProjectilePool.CreatePool();
                    _projectilePool = newProjectilePool;
                }
            }
            return _projectilePool;
        }
    }


    public virtual void Shoot(Vector3 position, Quaternion direction, int layer, Transform target = null)
    {
        if (target == null) target = projectilePool.playerTransform;
        Projectile newProjectile = projectilePool.Get();
        newProjectile.Shoot(position, direction, layer, target);
    }

    [Button]
    public virtual void ShootFromShooter(Transform target = null)
    {
        Shoot(transform.position, transform.rotation, LayerMask.NameToLayer("EnemiesProjectiles"), target);
    }

}
