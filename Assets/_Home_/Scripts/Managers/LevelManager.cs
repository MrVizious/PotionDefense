using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using ExtensionMethods;

public class LevelManager : StateMachine<LevelState>
{

    public float experience = 0f;
    public float currentFortressHealth
    {
        get => _currentFortressHealth;
        set
        {
            value = Mathf.Max(0, value);
            _currentFortressHealth = value;
            if (_currentFortressHealth <= 0)
            {
                FortressDied();
            }
        }
    }
    public int currentWaveIndex = 0;
    private PlayerController player;
    private float _currentFortressHealth = 50f;

    protected override void Awake()
    {
        base.Awake();
        if (player == null) player = FindObjectOfType<PlayerController>();
        player.onDie += PlayerDied;

        currentWaveIndex = 0;
        experience = 100000f;
        ChangeToState(this.GetOrAddComponent<BuyRoundState>());
    }

    public void PlayerDied()
    {
        Debug.Log("Player just died!");
        SceneManagerSingleton.Instance.GoToLoseMenu();
    }

    public void FortressDied()
    {
        Debug.Log("Fortress died!");
        SceneManagerSingleton.Instance.GoToLoseMenu();
    }
}