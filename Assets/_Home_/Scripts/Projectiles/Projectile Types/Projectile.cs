using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class Projectile : Poolable
{
    public ProjectileData projectileData;
    public float speedModifier = 1f;
    public float damageModifier = 1f;
    public abstract void Move();
    public abstract void Shoot(Vector3 position, Quaternion rotation, int layer, Transform target = null);
    public abstract void OnCollisionEnter2D(Collision2D other);
}