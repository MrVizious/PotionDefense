using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Potion Defense/ProjectileData", order = 0)]
public class ProjectileData : ScriptableObject
{
    public float speed;
    public float damage;
    public float secondsToDie;
}