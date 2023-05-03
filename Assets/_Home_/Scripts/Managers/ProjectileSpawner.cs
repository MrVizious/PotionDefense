using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;

public class ProjectileSpawner : MonoBehaviour
{
    public Projectile projectilePrefab;
    private Pool<Projectile> projectilePool;
    private void Start()
    {
        projectilePool = new Pool<Projectile>(
            3, 50, prefab: projectilePrefab
        );
    }

    [Button]
    public void Spawn()
    {
        Projectile newProjectile = projectilePool.Get();
        newProjectile.transform.position = transform.position;
        newProjectile.transform.rotation = transform.rotation;
        newProjectile.Init(projectilePool);
    }
}
