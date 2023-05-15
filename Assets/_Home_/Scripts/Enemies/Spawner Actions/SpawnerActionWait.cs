using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class SpawnerActionWait : SpawnerAction
{
    public float secondsToWait;
    public override void Begin()
    {
        Debug.Log("Going to wait for " + secondsToWait + " seconds!");
        Wait((int)(secondsToWait * 1000)).Forget();
    }

    private async UniTaskVoid Wait(int millisecondsToWait)
    {
        await UniTask.Delay(millisecondsToWait);
        onEnd.Invoke();
    }
}
