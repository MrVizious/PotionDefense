using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using ExtensionMethods;
using Sirenix.OdinInspector;
using DesignPatterns;
using UltEvents;

public class Enemy : Poolable<Enemy>
{
    // Events
    public UltEvent onDie;
    public UltEvent onEndOfPath;

    // Public Data
    public EnemyData data;
    public PathCreator path;

    // Properties
    private float _currentHealth;
    [ShowInInspector]
    public float currentHealth
    {
        get => _currentHealth;
        protected set
        {
            value = Mathf.Clamp(value, 0f, Mathf.Infinity);
            _currentHealth = value;
            if (_currentHealth <= 0f) onDie.Invoke();
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
        if (onDie == null) onDie = new UltEvent();
        onDie += Release;
    }

    [Button]
    public void Hurt(float hurtAmount)
    {
        currentHealth -= hurtAmount;
    }

    public void Init(Pool<Enemy> newPool, PathCreator newPath)
    {
        base.Init(newPool);
        currentHealth = data.maxHealth;
        path = newPath;
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
        distanceTravelled += data.speed * Time.deltaTime;
        transform.position = path.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = path.path.GetRotationAtDistance(distanceTravelled);
    }
}