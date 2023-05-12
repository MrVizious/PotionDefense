using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;

public class ShooterController : MonoBehaviour
{

    public EnemyData enemyData;

    private float secondsSinceLastShot = 0f;
    private List<SimpleProjectileSpawner> simpleProjectileSpawners;
    private List<FollowingProjectileSpawner> followingProjectileSpawners;
    private List<PredictiveProjectileSpawner> predictiveProjectileSpawners;



    private void Awake()
    {
        simpleProjectileSpawners = new List<SimpleProjectileSpawner>(this.GetComponentsInChildren<SimpleProjectileSpawner>());
        followingProjectileSpawners = new List<FollowingProjectileSpawner>(this.GetComponentsInChildren<FollowingProjectileSpawner>());
        predictiveProjectileSpawners = new List<PredictiveProjectileSpawner>(this.GetComponentsInChildren<PredictiveProjectileSpawner>());
    }

    private void Update()
    {
        secondsSinceLastShot += Time.deltaTime;
        if (secondsSinceLastShot > enemyData.secondsBetweenShots) Shoot();
    }

    [Button]
    public void Shoot()
    {
        foreach (SimpleProjectileSpawner spawner in simpleProjectileSpawners)
        {
            spawner.ShootFromShooter();
        }
        foreach (FollowingProjectileSpawner spawner in followingProjectileSpawners)
        {
            spawner.ShootFromShooter();
        }
        foreach (PredictiveProjectileSpawner spawner in predictiveProjectileSpawners)
        {
            spawner.ShootFromShooter();
        }
        secondsSinceLastShot = 0f;
    }
}
