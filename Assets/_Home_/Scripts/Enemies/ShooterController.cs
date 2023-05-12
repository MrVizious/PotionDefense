using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;

public class ShooterController : MonoBehaviour
{

    public EnemyData enemyData;

    private float secondsSinceLastShot = 0f;
    private List<ProjectileSpawner> projectileSpawners;



    private void Awake()
    {
        projectileSpawners = new List<ProjectileSpawner>(this.GetComponentsInChildren<ProjectileSpawner>());
    }

    private void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
        if (secondsSinceLastShot > enemyData.secondsBetweenShots) Shoot();
    }

    [Button]
    public void Shoot()
    {
        foreach (ProjectileSpawner spawner in projectileSpawners)
        {
            spawner.ShootFromShooter();
        }
        secondsSinceLastShot = 0f;
    }
}
