using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;
using UnityEngine.InputSystem;
using ExtensionMethods;
using Sirenix.OdinInspector;
using UtilityMethods;
using UnityEngine.UI;

public class TowerSpot : MonoBehaviour
{
    public GameObject promptSign;
    public Tower tower
    {
        set
        {
            _tower = value;
        }
        get
        {
            if (_tower == null) _tower = GetComponentInChildren<Tower>();
            return _tower;
        }
    }


    [SerializeField]
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

    private PlayerInput _playerInput;
    private PlayerInput playerInput
    {
        get
        {
            if (_playerInput == null) _playerInput = FindObjectOfType<PlayerInput>();
            return _playerInput;
        }
    }

    private void Start()
    {
        playerInput.actions["Interact"].performed += InteractButtonPressed;
        playerInput.actions["Look"].performed += ChooseActionWithJoystick;
        playerInput.actions["Back"].performed += BackButtonPressed;
        wheel = GetComponentInChildren<OptionsWheel>();
        ChangeToHidden();
    }

    public void ChangeToHidden()
    {
        if (state != TowerSpotState.Hidden)
        {
            state = TowerSpotState.Hidden;
            return;
        }
        promptSign.SetActive(false);
        wheel.gameObject.SetActive(false);
    }

    public void ChangeToPrompt()
    {
        if (player == null) return;
        if (state != TowerSpotState.Prompt)
        {
            state = TowerSpotState.Prompt;
            return;
        }
        promptSign.SetActive(true);
        player.ChangeToPreviousState();
        wheel.gameObject.SetActive(false);
    }

    public void ChangeToSelecting()
    {
        if (player == null) return;
        if (state != TowerSpotState.Selecting)
        {
            state = TowerSpotState.Selecting;
            return;
        }
        promptSign.SetActive(false);
        wheel.gameObject.SetActive(true);
        player.ChangeToState(player.GetOrAddComponent<UIState>());
        wheel.ClearActions();
        // If there is a tower
        if (tower != null)
        {
            wheel.AddAction(typeof(EvolveWheelAction));
            wheel.AddAction(typeof(SellWheelAction));
        }
        else if (tower == null)
        {
            wheel.AddAction(typeof(BuyIceTowerWheelAction));
            wheel.AddAction(typeof(BuyFireTowerWheelAction));
            wheel.AddAction(typeof(BuyShieldTowerWheelAction));
            wheel.AddAction(typeof(BuyEarthTowerWheelAction));
            wheel.AddAction(typeof(BuyElectricityTowerWheelAction));
        }
        wheel.RenderSectors();
    }

    public void InteractButtonPressed(InputAction.CallbackContext c)
    {
        if (player == null) return;
        if (c.performed)
        {
            if (state == TowerSpotState.Prompt)
            {
                ChangeToSelecting();
            }
            else if (state == TowerSpotState.Selecting)
            {
                if (playerInput.currentControlScheme.ToLower().Equals("keyboard and mouse"))
                {
                    ChangeToPrompt();
                }
            }
        }
    }

    public void BackButtonPressed(InputAction.CallbackContext c)
    {
        if (player == null) return;
        if (state == TowerSpotState.Prompt)
        {
            ChangeToHidden();
        }
        else if (state == TowerSpotState.Selecting)
        {
            ChangeToPrompt();
        }
    }

    public void ChooseActionWithJoystick(InputAction.CallbackContext c)
    {
        if (player == null) return;
        if (state != TowerSpotState.Selecting) { return; }

        // Only use this if using a gamepad
        if (!playerInput.currentControlScheme.ToLower().Equals("gamepad"))
        {
            return;
        }

        if (!c.performed) return;

        Vector2 lookVector = c.ReadValue<Vector2>().normalized;

        WheelSector pointedSector = wheel.sectors[0];
        float closestToDirection = -1f;
        for (int i = 0; i < wheel.sectors.Count; i++)
        {
            Vector2 iconPosition = wheel.sectors[i].icon.transform.position - transform.position;
            float currentSimilarity = Vector2.Dot(lookVector, iconPosition);
            if (currentSimilarity > closestToDirection)
            {
                closestToDirection = currentSimilarity;
                pointedSector = wheel.sectors[i];
            }
        }
        pointedSector.button.Select();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController newPlayer = other.GetComponent<PlayerController>();
        if (newPlayer == null) return;
        player = newPlayer;
        ChangeToPrompt();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController exitingPlayer = other.GetComponent<PlayerController>();
        if (player != exitingPlayer) return;
        player = null;
        ChangeToHidden();
    }
}