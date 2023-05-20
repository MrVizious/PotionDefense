using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellWheelAction : OptionsWheelAction
{
    public override void Execute(TowerSpot spot)
    {
        Destroy(spot.tower.gameObject);
        spot.ChangeToPrompt();
        // TODO: Receive money
    }
}
