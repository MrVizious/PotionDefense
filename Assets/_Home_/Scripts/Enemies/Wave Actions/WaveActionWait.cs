using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class WaveActionWait : WaveAction
{
    public float secondsToWait;
    public override void Begin(EnemySpawner newSpawner)
    {
        base.Begin(newSpawner);
        Wait((int)(secondsToWait * 1000)).Forget();
    }

    private async UniTaskVoid Wait(int millisecondsToWait)
    {
        await UniTask.Delay(millisecondsToWait);
        onEnd.Invoke();
    }
}
