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
        spot.tower.Evolve();
        if (spot.tower.CanEvolve())
        {
            spot.ChangeToSelecting();
        }
        else
        {
            spot.ChangeToPrompt();
        }
    }
}
