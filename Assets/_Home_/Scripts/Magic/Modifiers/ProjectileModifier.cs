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

    protected virtual void Start()
    {
        OnAcquire();
    }
    public virtual void OnAcquire() { }
    public virtual void OnHit(Enemy enemy)
    {
        if (enemy == null) return;
        if (!enemy.gameObject.activeInHierarchy) return;
    }
}
