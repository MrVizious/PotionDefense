using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

[System.Serializable]
public abstract class WaveAction
{
    public UltEvent onEnd;
    protected EnemySpawner spawner;

    public virtual void Begin(EnemySpawner newSpawner)
    {
        spawner = newSpawner;
    }
}
