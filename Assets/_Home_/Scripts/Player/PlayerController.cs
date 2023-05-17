using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;
using UnityEngine.InputSystem;
using DesignPatterns;
using UltEvents;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerLook))]
public class PlayerController : StateMachine<PlayerState>, IDamageable
{

    public PlayerData playerData;
    [HideInInspector] public Rigidbody2D rb { get; private set; }
    [HideInInspector] public PlayerLook playerLook { get; private set; }

    private float _currentHealth;
    [ShowInInspector]
    public float currentHealth
    {
        get { return _currentHealth; }
        private set
        {
            value = Mathf.Max(0f, value);
            value = Mathf.Min(playerData.maxHealth, value);
            if (value > _currentHealth) onHealed?.Invoke();
            if (value < _currentHealth) onDamaged?.Invoke();
            _currentHealth = value;
            if (_currentHealth <= 0f)
            {
                onDie?.Invoke();
            }
        }
    }

    [ShowInInspector]
    public UltEvent onDamaged { get; set; }
    [ShowInInspector]
    public UltEvent onHealed { get; set; }
    [ShowInInspector]
    public UltEvent onDie { get; set; }

    [HideInInspector] public Vector2 lastMovementInput = Vector2.zero;

    [HideInInspector] public HashSet<Collider2D> touchingColliders;

    protected override void Awake()
    {
        base.Awake();
        touchingColliders = new HashSet<Collider2D>();
    }
    private void Start()
    {
        rb = this.GetOrAddComponent<Rigidbody2D>();
        playerLook = this.GetOrAddComponent<PlayerLook>();
        ChangeToState(this.GetOrAddComponent<IdleState>());
        _currentHealth = playerData.maxHealth;
        if (onDie == null) onDie = new UltEvent();
    }

    public override void ChangeToPreviousState()
    {
        if (stateStack.Count <= 1)
        {
            ChangeToState(this.GetOrAddComponent<IdleState>());
        }
        else
        {
            base.ChangeToPreviousState();
        }
    }

    public void Movement(InputAction.CallbackContext c)
    {
        if (c.canceled)
        {
            lastMovementInput = Vector2.zero;
        }
        else
        {
            lastMovementInput = c.ReadValue<Vector2>();
        }
        currentState.Move(c);
    }

    public void Dash(InputAction.CallbackContext c)
    {
        if (c.started)
            currentState.Dash();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        touchingColliders.Add(other.collider);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        touchingColliders.Remove(other.collider);
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
    }
}
