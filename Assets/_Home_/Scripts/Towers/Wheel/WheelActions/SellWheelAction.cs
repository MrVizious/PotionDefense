using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellWheelAction : OptionsWheelAction
{
    protected override string iconRoute
    {
        get => "Art/Sprites/UI/sell";
    }
    public override void Execute(TowerSpot spot)
    {
        Destroy(spot.tower.gameObject);
        spot.ChangeToPrompt();
        // TODO: Receive money
    }
}
