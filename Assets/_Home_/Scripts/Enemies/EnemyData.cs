using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Potion Defense/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public float speed;

}