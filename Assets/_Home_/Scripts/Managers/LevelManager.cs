using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class LevelManager : StateMachine<LevelState>
{
    public PlayerController player;
    public LevelData levelData;

    private void Awake()
    {
        if (player == null) player = FindObjectOfType<PlayerController>();
        player.onDie += PlayerDied;
    }

    public void PlayerDied()
    {
        Debug.Log("Player just died!");
    }
}
