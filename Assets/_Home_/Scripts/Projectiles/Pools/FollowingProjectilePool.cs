using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class FollowingProjectilePool : ProjectilePool<FollowingProjectile>
{
    public override Pool<FollowingProjectile> projectilePool
    {
        get;
        protected set;
    }
    protected override void OnEnable()
    {
        projectilePool = new Pool<FollowingProjectile>(
            3, 50, prefab: projectilePrefab
        );
    }
}