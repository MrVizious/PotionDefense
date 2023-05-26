using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveWheelAction : OptionsWheelAction
{
    protected override string iconRoute
    {
        get => "Art/Sprites/UI/evolve";
    }
    public override void Execute(TowerSpot spot)
    {
        if (spot.tower.CanEvolve())
        {
            spot.tower.Evolve();
            spot.ChangeToSelecting();
        }
        else
        {
            spot.ChangeToPrompt();
        }
    }
}
