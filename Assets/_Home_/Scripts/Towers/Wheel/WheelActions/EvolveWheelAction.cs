using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EvolveWheelAction : OptionsWheelAction
{
    public override void Execute(TowerSpot spot)
    {
        spot.tower.Evolve();
    }
}
