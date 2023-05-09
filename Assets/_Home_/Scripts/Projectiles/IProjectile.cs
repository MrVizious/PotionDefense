using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public interface IProjectile
{
    float speed { get; }
    float speedModifier { get; }
    float damage { get; }
    float damageModifier { get; }
    float secondsToDie { get; }
    void Move();
    void Shoot(Vector3 position, Quaternion rotation, int layer, Transform target = null);
    void OnCollisionEnter2D(Collision2D other);
}