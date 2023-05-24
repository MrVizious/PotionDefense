using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Potion Defense/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public float maxHealth = 10f;
    public float speed = 1f;
    public float secondsBetweenShots = 1f;
    public float damageToFortress = 1f;

}