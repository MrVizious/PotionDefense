using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using ExtensionMethods;
using Sirenix.OdinInspector;
using DesignPatterns;
using UltEvents;

public class Enemy : Poolable, IDamageable
{
    // Events
    public UltEvent onDie { get; set; }
    public UltEvent onDamaged { get; set; }
    public UltEvent onHealed { get; set; }
    public UltEvent onEndOfPath;

    // Public Data
    public EnemyData enemyData;
    public PathCreator path;
    public float speedModifier = 1f;

    // Properties
    private float _currentHealth;
    [ShowInInspector]
    public float currentHealth
    {
        get { return _currentHealth; }
        protected set
        {
            value = Mathf.Max(0f, value);
            value = Mathf.Min(enemyData.maxHealth, value);
            if (value > _currentHealth) onHealed?.Invoke();
            if (value < _currentHealth) onDamaged?.Invoke();
            _currentHealth = value;
            if (_currentHealth <= 0f)
            {
                onDie?.Invoke();
            }
        }
    }

    private bool _moving;
    [ShowInInspector]
    public bool moving
    {
        get => _moving;
        set
        {
            if (value == true) distanceTravelled = 0f;
            _moving = value;
        }
    }


    // Private variables
    private float distanceTravelled = 0f;

    private void Start()
    {
        _currentHealth = enemyData.maxHealth;
        if (onDie == null) onDie = new UltEvent();
        onDie += Release;
    }

    [Button]
    public void Damage(float hurtAmount)
    {
        currentHealth -= hurtAmount;
    }
    public void Heal(float hurtAmount)
    {
        currentHealth += hurtAmount;
    }

    public override void OnPoolGet()
    {
        base.OnPoolGet();
        currentHealth = enemyData.maxHealth;
        gameObject.SetActive(true);
        moving = true;
    }
    public override void OnPoolRelease()
    {
        Debug.Log("Releasing");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (moving)
        {
            Move();
            if (path.path.HasReachedEndAtDistance(distanceTravelled)) Release();
        }
    }

    private void Move()
    {
        distanceTravelled += enemyData.speed * speedModifier * Time.deltaTime;
        transform.position = path.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = path.path.GetRotationAtDistance(distanceTravelled);
    }



}