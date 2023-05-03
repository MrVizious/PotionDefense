using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Potion Defense/TowerData", order = 0)]
public class TowerData : ScriptableObject
{
    public float maxHealth;
    public float range;
    public TowerData nextLevel;

}