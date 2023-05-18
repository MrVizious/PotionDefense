using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class WaveActionRepeatedSpawn : WaveAction
{
    public Enemy enemyPrefab;
    public float secondsBetweenSpawns;
    public int enemiesToSpawn;
    [HideInInspector]

    public override void Begin(EnemySpawner newSpawner)
    {
        base.Begin(newSpawner);
        RepeatedSpawn().Forget();
    }

    private async UniTaskVoid RepeatedSpawn()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            spawner.Spawn(enemyPrefab);
            await UniTask.Delay((int)secondsBetweenSpawns * 1000);
        }
        onEnd.Invoke();
    }
}