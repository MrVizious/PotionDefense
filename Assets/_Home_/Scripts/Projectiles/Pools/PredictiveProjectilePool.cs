using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class PredictiveProjectilePool : ProjectilePool<PredictiveProjectile>
{

    public override Pool<PredictiveProjectile> projectilePool
    {
        get;
        protected set;
    }
    protected override void OnEnable()
    {
        projectilePool = new Pool<PredictiveProjectile>(
            3, 50, prefab: projectilePrefab
        );
    }
}