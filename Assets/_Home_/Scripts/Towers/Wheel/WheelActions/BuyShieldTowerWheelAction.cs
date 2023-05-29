using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShieldTowerWheelAction : BuyTowerWheelAction
{
    protected override string towerRoute
    {
        get => "Prefabs/Towers/Shield Tower";
    }

    protected override string iconRoute
    {
        get => "Art/Sprites/UI/shield";
    }
}
