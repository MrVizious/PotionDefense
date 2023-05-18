using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveActionSpawn : WaveAction
{
    public Enemy enemyPrefab;
    [HideInInspector]

    public override void Begin(EnemySpawner newSpawner)
    {
        base.Begin(newSpawner);
        spawner.Spawn(enemyPrefab);
        onEnd.Invoke();
    }
}