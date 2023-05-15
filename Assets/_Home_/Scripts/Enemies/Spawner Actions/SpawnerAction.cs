using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

[System.Serializable]
public abstract class SpawnerAction
{
    public UltEvent onEnd;

    public abstract void Begin();
}
