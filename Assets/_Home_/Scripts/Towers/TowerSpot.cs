using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;
using UnityEngine.InputSystem;
using ExtensionMethods;
using Sirenix.OdinInspector;
using UtilityMethods;

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

    private void Start()
    {
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        playerInput.actions["Interact"].performed += Select;
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
            if (tower.CanEvolve())
            {
                wheel.AddAction(typeof(EvolveWheelAction));
            }
            wheel.AddAction(typeof(SellWheelAction));
        }
        // TODO: If there is NO tower
        else if (tower == null)
        {
            wheel.AddAction(typeof(BuyIceTowerWheelAction));
        }
        wheel.RenderSectors();
    }

    public void Select(InputAction.CallbackContext c)
    {
        if (c.performed)
        {
            if (state == TowerSpotState.Prompt)
            {
                ChangeToSelecting();
            }
            else if (state == TowerSpotState.Selecting)
            {
                ChangeToPrompt();
            }
        }
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