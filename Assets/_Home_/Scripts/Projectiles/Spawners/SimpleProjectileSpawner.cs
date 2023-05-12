using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using UnityEngine.InputSystem;

public class SimpleProjectileSpawner : ProjectileSpawner<SimpleProjectile>
{

    protected override Pool<SimpleProjectile> projectilePool
    {
        get
        {
            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<SimpleProjectilePool>().projectilePool;
                if (_projectilePool == null) Debug.LogError("There is no projectile pool!");
            }
            return _projectilePool;
        }
    }
}