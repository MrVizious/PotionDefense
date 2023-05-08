using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UltEvents;

public interface IDamageable
{
    [ShowInInspector]
    float currentHealth { get; }


    UltEvent onDamaged { get; set; }
    UltEvent onDie { get; set; }


    [Button]
    void Damage(float amount)
    {
        Debug.Log("Damaged by " + amount);
    }
}
