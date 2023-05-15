using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActionSpawn : SpawnerAction
{
    public Enemy enemyPrefab;
    [HideInInspector]
    public EnemySpawner spawner;

    public override void Begin()
    {
        spawner.Spawn(enemyPrefab);
        onEnd.Invoke();
    }
}
