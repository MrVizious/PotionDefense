using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class PredictiveProjectileSpawner : ProjectileSpawner<PredictiveProjectile>
{
    private Transform _player;
    private Transform player
    {
        get
        {
            if (_player == null) _player = FindObjectOfType<PlayerController>().transform;
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    protected override Pool<PredictiveProjectile> projectilePool
    {
        get
        {
            if (_projectilePool == null)
            {
                _projectilePool = FindObjectOfType<PredictiveProjectilePool>().projectilePool;
                if (_projectilePool == null) Debug.LogError("There is no projectile pool!");
            }
            return _projectilePool;
        }
    }

    public override void ShootFromShooter(Transform target = null)
    {
        if (target == null) ShootFromShooterTowardsPlayer();
        else
        {
            base.ShootFromShooter(target);
        }
    }

    [Button]
    public virtual void ShootFromShooterTowardsPlayer()
    {
        Shoot(transform.position, transform.rotation, LayerMask.NameToLayer("EnemiesProjectiles"), player);
    }

}