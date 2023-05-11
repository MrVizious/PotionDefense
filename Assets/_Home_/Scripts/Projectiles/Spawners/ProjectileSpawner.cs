using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;

public abstract class ProjectileSpawner<T> : MonoBehaviour where T : Poolable, IProjectile
{
    protected Pool<T> _projectilePool;
    protected Pool<T> projectilePool
    {
        get
        {
            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<ProjectilePool<T>>().projectilePool;
                if (_projectilePool == null) Debug.LogError("There is no projectile pool!");
            }
            return _projectilePool;
        }
    }


    public virtual void Shoot(Vector3 position, Quaternion direction, int layer, Transform target = null)
    {
        T newProjectile = projectilePool.Get();
        newProjectile.Shoot(position, direction, layer, target);
    }

    public virtual void ShootFromShooterTowards(Transform target = null)
    {
        Shoot(transform.position, transform.rotation, LayerMask.NameToLayer("EnemiesProjectiles"), target);
    }

}
