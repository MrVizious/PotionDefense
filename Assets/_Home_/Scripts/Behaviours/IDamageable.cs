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


    void Damage(float amount);
}
