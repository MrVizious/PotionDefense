using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using Sirenix.OdinInspector;
using ExtensionMethods;

public class BuyRoundState : LevelState
{

    [Button]
    public void BeginWave()
    {
        stateMachine.ChangeToState(this.GetOrAddComponent<WaveRunningState>());
    }
}
