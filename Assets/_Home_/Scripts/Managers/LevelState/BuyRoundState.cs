using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using ExtensionMethods;

public class BuyRoundState : LevelState
{

    [Button]
    public void BeginWave()
    {
        if (stateMachine.currentState != this) return;
        stateMachine.ChangeToState(this.GetOrAddComponent<WaveRunningState>());
    }

    public void BeginWave(InputAction.CallbackContext c)
    {
        BeginWave();
    }
}
