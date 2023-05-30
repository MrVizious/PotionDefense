using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyElectricityTowerWheelAction : BuyTowerWheelAction
{
    protected override string towerRoute
    {
        get => "Prefabs/Towers/Electricity Tower";
    }

    protected override string iconRoute
    {
        get => "Art/Sprites/UI/electricity";
    }
}
