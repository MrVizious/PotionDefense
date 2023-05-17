using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Potion Defense/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public List<SpawnSequence> waves;
}