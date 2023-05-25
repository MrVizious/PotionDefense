using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuyTowerWheelAction : OptionsWheelAction
{
    protected virtual string towerRoute
    {
        get => "";
    }

    public override void Execute(TowerSpot spot)
    {
        if (spot.tower != null) return;
        GameObject towerPrefab = Resources.Load<GameObject>(towerRoute);
        Tower newTower = Instantiate(towerPrefab, spot.transform).GetComponentInChildren<Tower>();
        //spot.tower = newTower;
        spot.ChangeToSelecting();
    }
}
