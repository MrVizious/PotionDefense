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

    [Button]
    public virtual void ShootFromShooterTowardsPlayer()
    {
        Debug.Log("player from predictive spawner:" + player);
        Shoot(transform.position, transform.rotation, LayerMask.NameToLayer("EnemiesProjectiles"), player);
    }

}