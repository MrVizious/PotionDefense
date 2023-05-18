using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using ExtensionMethods;

public class LevelManager : StateMachine<LevelState>
{
    public int currentWaveIndex = 0;
    private PlayerController player;

    protected override void Awake()
    {
        base.Awake();
        if (player == null) player = FindObjectOfType<PlayerController>();
        player.onDie += PlayerDied;

        currentWaveIndex = 0;
        ChangeToState(this.GetOrAddComponent<BuyRoundState>());
    }

    public void PlayerDied()
    {
        Debug.Log("Player just died!");
    }
}
