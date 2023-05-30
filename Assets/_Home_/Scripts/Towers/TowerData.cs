using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

[CreateAssetMenu(fileName = "TowerData", menuName = "Potion Defense/TowerData", order = 0)]
public class TowerData : ScriptableObject
{
    [Inherits(typeof(ProjectileModifier))]
    public TypeReference projectileModifierType;
    public float maxHealth;
    public float range;
    public float cost;
    [Range(0f, 1f)]
    public float effectChance = 1f;
    public float effectDamageModifier = 1.0f;
    public float effectSpeedModifier = 1f;
    public float effectDurationInSeconds = 0f;
    public TowerData nextLevel;
    public Color color;
}