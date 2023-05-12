using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;

public abstract class ProjectileSpawner<T> : MonoBehaviour where T : Projectile
{
    protected Pool<T> _projectilePool;
    protected abstract Pool<T> projectilePool { get; }
    /*
    {
        get
        {
            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<ProjectilePool>().projectilePool;
                if (_projectilePool == null) Debug.LogError("There is no projectile pool!");
            }
            return _projectilePool;
        }
    }
    */


    public virtual void Shoot(Vector3 position, Quaternion direction, int layer, Transform target = null)
    {
        var newProjectile = projectilePool.Get();
        newProjectile.Shoot(position, direction, layer, target);
    }

    public virtual void ShootFromShooter(Transform target = null)
    {
        Shoot(transform.position, transform.rotation, LayerMask.NameToLayer("EnemiesProjectiles"), target);
    }

}
