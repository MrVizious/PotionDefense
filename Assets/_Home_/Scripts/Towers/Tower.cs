using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using Sirenix.OdinInspector;
using System.Linq;

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
        data = data.nextLevel;
        UpdateRadius();
    }

    public bool CanEvolve()
    {
        return data.nextLevel != null;
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
        SimpleProjectile projectile = other.GetComponent<SimpleProjectile>();
        if (projectile == null) return;
        projectile.GetOrAddComponent(data.projectileModifierType);
    }
}

