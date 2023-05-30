using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public abstract class ProjectileModifier : MonoBehaviour
{
    protected Projectile projectile;
    protected abstract TypeReference effectType
    {
        get;
    }
    public TowerData data;
    protected void OnEnable()
    {
        projectile = GetComponent<Projectile>();
    }

    protected void Start()
    {
        OnAcquire();
    }

    public virtual void OnAcquire() { }
    public virtual void OnHit(Enemy enemy)
    {
        if (enemy == null) return;
        if (!enemy.gameObject.activeInHierarchy) return;
        if (effectType == null) return;

        // Stops any previous active effect of the type to the enemy
        Effect currentEffect = (Effect)enemy.gameObject.GetComponent(effectType);
        if (currentEffect != null)
        {
            Destroy(currentEffect);
        }

        // Adds the new effect
        Effect newEffect = (Effect)enemy.gameObject.AddComponent(effectType);
        newEffect.Begin(data);
    }

    public virtual void End() { }

    protected virtual void OnDestroy()
    {
        End();
    }
}