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
    public TowerData nextLevel;

}