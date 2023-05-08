using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileModifier : MonoBehaviour
{
    protected Projectile projectile;
    protected void OnEnable()
    {
        projectile = GetComponent<Projectile>();
    }

    public virtual void OnAcquire() { }
    public virtual void OnHit(Enemy enemy) { }
}
