using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;
using UnityEngine.InputSystem;

public class TowerSpot : MonoBehaviour
{

    public GameObject promptSign;
    public Tower tower
    {
        get
        {
            if (_tower == null) _tower = GetComponentInChildren<Tower>();
            return _tower;
        }
    }


    private Tower _tower;
    private PlayerController player;
    private OptionsWheel wheel;
    private enum TowerSpotState
    {
        Hidden,
        Prompt,
        Selecting
    }
    private TowerSpotState _state;
    private TowerSpotState state
    {
        get => _state;
        set
        {
            _state = value;
            switch (_state)
            {
                case TowerSpotState.Hidden:
                    ChangeToHidden();
                    break;
                case TowerSpotState.Prompt:
                    ChangeToPrompt();
                    break;
                case TowerSpotState.Selecting:
                    ChangeToSelecting();
                    break;
            }
        }
    }

    private void Start()
    {
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        playerInput.actions["Interact"].performed += Select;
        wheel = GetComponentInChildren<OptionsWheel>();
        ChangeToHidden();
    }

    private void ChangeToHidden()
    {
        if (state != TowerSpotState.Hidden)
        {
            state = TowerSpotState.Hidden;
            return;
        }
        promptSign.SetActive(false);
        wheel.gameObject.SetActive(false);
    }

    private void ChangeToPrompt()
    {
        if (state != TowerSpotState.Prompt)
        {
            state = TowerSpotState.Prompt;
            return;
        }
        promptSign.SetActive(true);
        wheel.gameObject.SetActive(false);
    }

    private void ChangeToSelecting()
    {
        if (state != TowerSpotState.Selecting)
        {
            state = TowerSpotState.Selecting;
            return;
        }
        promptSign.SetActive(false);
        wheel.gameObject.SetActive(true);
        wheel.ClearActions();
        if (tower != null)
        {
            if (tower.CanEvolve())
            {
                wheel.AddAction(typeof(EvolveWheelAction));
            }
            wheel.AddAction(typeof(SellWheelAction));
        }
    }

    public void Select(InputAction.CallbackContext c)
    {
        if (c.performed)
        {
            if (state == TowerSpotState.Prompt)
            {
                ChangeToSelecting();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<PlayerController>();
        if (player == null) return;
        ChangeToPrompt();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController exitingPlayer = other.GetComponent<PlayerController>();
        if (player != exitingPlayer) return;
        ChangeToHidden();
    }
}