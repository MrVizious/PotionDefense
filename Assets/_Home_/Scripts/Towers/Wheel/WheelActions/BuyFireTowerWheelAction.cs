using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFireTowerWheelAction : BuyTowerWheelAction
{
    protected override string towerRoute
    {
        get => "Prefabs/Towers/Fire Tower";
    }

    protected override string iconRoute
    {
        get => "Art/Sprites/UI/fire";
    }
}
