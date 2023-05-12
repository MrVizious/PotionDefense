using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public abstract class Projectile : PoolableGO
{
    public float speed;
    public float speedModifier;
    public float damage;
    public float damageModifier;
    public float secondsToDie;
    public abstract void Move();
    public abstract void Shoot(Vector3 position, Quaternion rotation, int layer, Transform target = null);
    public abstract void OnCollisionEnter2D(Collision2D other);
}