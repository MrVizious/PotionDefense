using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class SpawnSequence : MonoBehaviour
{
    [SerializeReference]
    public List<SpawnerAction> spawnerActions;
    private int currentActionIndex = 0;

    private void Start()
    {
        currentActionIndex = 0;
        ExecuteCurrentAction();
    }

    private void ExecuteCurrentAction()
    {
        if (currentActionIndex >= spawnerActions.Count) return;
        spawnerActions[currentActionIndex].onEnd += ExecuteNextAction;
        spawnerActions[currentActionIndex].Begin();
    }

    private void ExecuteNextAction()
    {
        spawnerActions[currentActionIndex].onEnd -= ExecuteNextAction;
        currentActionIndex++;
        ExecuteCurrentAction();
    }
}