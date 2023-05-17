using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using ExtensionMethods;

public class LevelManager : StateMachine<LevelState>
{
    public PlayerController player;
    public LevelData levelData;

    protected override void Awake()
    {
        base.Awake();
        if (player == null) player = FindObjectOfType<PlayerController>();
        player.onDie += PlayerDied;

        ChangeToState(this.GetOrAddComponent<LevelStartState>());
    }

    public void PlayerDied()
    {
        Debug.Log("Player just died!");
    }
}
