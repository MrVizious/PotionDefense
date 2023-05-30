using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyEarthTowerWheelAction : BuyTowerWheelAction
{
    protected override string towerRoute
    {
        get => "Prefabs/Towers/Earth Tower";
    }

    protected override string iconRoute
    {
        get => "Art/Sprites/UI/earth";
    }
}
