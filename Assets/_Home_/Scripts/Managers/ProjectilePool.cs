using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class ProjectilePool : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Pool<Projectile> projectilePool
    {
        get;
        private set;
    }
    private void OnEnable()
    {
        projectilePool = new Pool<Projectile>(
            3, 50, prefab: projectilePrefab
        );
    }
}
