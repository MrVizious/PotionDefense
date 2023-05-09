using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public abstract class Shooter<T> : MonoBehaviour where T : Poolable<T>, IProjectile
{
    private Pool<T> _projectilePool;
    private Pool<T> projectilePool
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


    public void Shoot(Vector3 position, Quaternion direction, int layer, Transform target = null)
    {
        T newProjectile = projectilePool.Get();
        newProjectile.Init(projectilePool);
        newProjectile.Shoot(position, direction, layer, target);
    }
}
