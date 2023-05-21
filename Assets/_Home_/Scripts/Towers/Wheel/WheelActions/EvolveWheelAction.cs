using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveWheelAction : OptionsWheelAction
{
    public override void Execute(TowerSpot spot)
    {
        spot.tower.Evolve();
        spot.ChangeToSelecting();
    }
}
