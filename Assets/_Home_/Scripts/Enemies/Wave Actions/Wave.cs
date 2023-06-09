using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "SpawnSequence", menuName = "Potion Defense/SpawnSequence", order = 0)]
[System.Serializable]
public class Wave : ScriptableObject
{
    [SerializeReference]
    public List<WaveAction> waveActions;
}