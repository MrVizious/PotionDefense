using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using Sirenix.OdinInspector;
using System.Linq;
using TypeReferences;

public class Tower : MonoBehaviour
{
    public TowerData data;
    private SpriteRenderer radiusRenderer;

    private void Start()
    {
        //SpriteRenderer[] renderers = GetComponentInChildren<SpriteRenderer>().Where(go => go.gameObject != this.gameObject);
        radiusRenderer = this.GetComponentInChildrenExcludingParent<SpriteRenderer>();
        UpdateRadius();
    }

    [Button]
    public void Evolve()
    {
        if (!CanEvolve()) return;
        FindObjectOfType<LevelManager>().experience -= CostToEvolve();
        data = data.nextLevel;
        UpdateRadius();
    }

    public bool CanEvolve()
    {
        float cost = CostToEvolve();
        if (cost < 0) return false;
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (cost <= levelManager.experience)
        {
            return true;
        }
        return false;
    }

    public float CostToEvolve()
    {
        if (data.nextLevel == null) return -1;
        return data.nextLevel.cost;
    }

    private void UpdateRadius()
    {
        if (data == null) return;
        SetRadius(data.range);
    }
    private void SetRadius(float r)
    {
        radiusRenderer.transform.localScale = Vector3.one * r * 2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile == null) return;
        if (data.projectileModifierType == null) return;


        // If the current modifier type is already in the projectile, substitute it
        ProjectileModifier currentModifier =
            (ProjectileModifier)projectile.GetComponent(data.projectileModifierType);
        if (currentModifier != null)
        {
            Destroy(currentModifier);
            Debug.Log("Adding same component");
            projectile.AddComponent(data.projectileModifierType);
            return;
        }

        // Ice neutralizes fire
        if (data.projectileModifierType.Type == typeof(IceModifier))
        {
            FireModifier fireModifier = projectile.GetComponent<FireModifier>();
            if (fireModifier != null)
            {
                Destroy(fireModifier);
                return;
            }
        }

        // Fire neutralizes ice
        if (data.projectileModifierType.Type == typeof(FireModifier))
        {
            IceModifier iceModifier = projectile.GetComponent<IceModifier>();
            if (iceModifier != null)
            {
                Destroy(iceModifier);
                return;
            }
        }

        ProjectileModifier newModifier =
            (ProjectileModifier)projectile.AddComponent(data.projectileModifierType);
        newModifier.data = data;

    }
}

