using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;
using UltEvents;
using ExtensionMethods;

public class WaveRunningState : LevelState
{
    public UltEvent onWaveEnded = new UltEvent();
    private List<EnemySpawner> spawners;
    private int _endedSpawnersCounter;
    private int endedSpawnersCounter
    {
        get => _endedSpawnersCounter;
        set
        {
            _endedSpawnersCounter = value;
            if (endedSpawnersCounter >= spawners.Count)
            {
                OnWaveEnded();
            }
        }
    }

    public override void Enter(StateMachine<LevelState> newStateMachine)
    {
        base.Enter(newStateMachine);
        spawners = new List<EnemySpawner>(FindObjectsOfType<EnemySpawner>());
        if (spawners.Count <= 0) Debug.LogError("No spawners found!");
        if (((LevelManager)stateMachine).currentWaveIndex >= spawners[0].levelData.waves.Count)
        {
            stateMachine.ChangeToState(this.GetOrAddComponent<LevelEndedState>());
            return;
        }
        PrepareWaveEndedListeners();
        StartWave();
    }

    private void StartWave()
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.ExecuteWave(((LevelManager)stateMachine).currentWaveIndex);
        }
    }

    private void PrepareWaveEndedListeners()
    {
        endedSpawnersCounter = 0;
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.onWaveEnded += AugmentEndedSpawnersCounter;
        }
    }

    private void AugmentEndedSpawnersCounter()
    {
        endedSpawnersCounter++;
    }

    private void OnWaveEnded()
    {
        Debug.Log("Wave ended!");
        onWaveEnded.Invoke();
        if (((LevelManager)stateMachine).currentWaveIndex >= spawners[0].levelData.waves.Count)
        {
            stateMachine.ChangeToState(this.GetOrAddComponent<LevelEndedState>());
            return;
        }
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.onWaveEnded -= AugmentEndedSpawnersCounter;
        }
        ((LevelManager)stateMachine).currentWaveIndex++;
        stateMachine.ChangeToState(this.GetOrAddComponent<BuyRoundState>());
    }
}
