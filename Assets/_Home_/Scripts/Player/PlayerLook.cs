using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ExtensionMethods;
using DesignPatterns;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField]
    private Transform target;
    [SerializeField]
    [Range(0, 20)]
    private float maxRange = 7f;
    private enum InputFormat
    {
        Mouse,
        Gamepad
    }
    private InputFormat lastInputFormat;
    private Vector2 lastMouseScreenPosition;
    private Vector2 lastJoystickInput;
    private Vector2 _targetPosition;
    private Vector2 targetPosition
    {
        get
        {
            if (lastInputFormat == InputFormat.Mouse)
            {
                _targetPosition = Camera.main.ScreenToWorldPoint(lastMouseScreenPosition);
                return Vector2.ClampMagnitude(_targetPosition - (Vector2)transform.position, maxRange) + (Vector2)transform.position;
            }
            else if (lastInputFormat == InputFormat.Gamepad)
            {
                return (Vector2)transform.position + lastJoystickInput * maxRange;
            }
            return Vector2.zero;
        }
        set
        {
            _targetPosition = value;
        }
    }

    public Vector2 lookDirection
    {
        get
        {
            return (targetPosition - (Vector2)transform.position).normalized;
        }
    }

    private ProjectileSpawner _shooter;
    private ProjectileSpawner shooter
    {
        get
        {
            if (_shooter == null)
            {
                _shooter = this.GetOrAddComponent<ProjectileSpawner>();
            }
            return _shooter;
        }
    }

    private void Update()
    {
        target.position = targetPosition;
        //target.up = -(targetPosition - (Vector2)transform.position);
    }

    public virtual void Look(InputAction.CallbackContext c)
    {
        Vector2 input = c.ReadValue<Vector2>();

        // Mouse controls
        if (playerInput.currentControlScheme.ToLower().Equals("keyboard and mouse"))
        {
            LookMouse(input);
        }
        // Gamepad controls
        else if (playerInput.currentControlScheme.ToLower().Equals("gamepad"))
        {
            LookGamepad(input);
        }
        // Whatever else
        else
        {
            Debug.Log(c.action.activeControl.device.path);
        }
    }

    protected virtual void LookMouse(Vector2 input)
    {
        lastInputFormat = InputFormat.Mouse;
        Vector3 mousePos = input;
        mousePos.z = transform.position.z;
        lastMouseScreenPosition = mousePos;
    }

    protected virtual void LookGamepad(Vector2 input)
    {
        lastInputFormat = InputFormat.Gamepad;
        lastJoystickInput = input;
    }

    protected virtual void Shoot(InputAction.CallbackContext c)
    {
        if (c.started)
            shooter.Shoot(transform.position,
                          Quaternion.LookRotation(transform.forward, lookDirection),
                          LayerMask.NameToLayer("PlayerProjectiles"));
    }
}
