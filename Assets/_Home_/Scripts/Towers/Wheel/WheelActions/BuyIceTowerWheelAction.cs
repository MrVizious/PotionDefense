using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyIceTowerWheelAction : BuyTowerWheelAction
{
    protected override string towerRoute
    {
        get => "Prefabs/Towers/Ice Tower";
    }

    protected override string iconRoute
    {
        get => "Art/Sprites/UI/snowflake";
    }
}
