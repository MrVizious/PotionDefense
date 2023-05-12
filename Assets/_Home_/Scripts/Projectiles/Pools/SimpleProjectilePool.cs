using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class SimpleProjectilePool : ProjectilePool<SimpleProjectile>
{
    public override Pool<SimpleProjectile> projectilePool
    {
        get;
        protected set;
    }
    protected override void OnEnable()
    {
        projectilePool = new Pool<SimpleProjectile>(
            3, 50, prefab: projectilePrefab
        );
    }
}