using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuyTowerWheelAction : OptionsWheelAction
{
    protected virtual string towerRoute
    {
        get => "";
    }

    public virtual GameObject towerPrefab
    {
        get
        {
            return Resources.Load<GameObject>(towerRoute);
        }
    }

    public override void Execute(TowerSpot spot)
    {
        if (spot.tower != null) return;
        float costToBuy = towerPrefab.GetComponent<Tower>().data.cost;
        if (costToBuy <= FindObjectOfType<LevelManager>().experience)
        {
            FindObjectOfType<LevelManager>().experience -= costToBuy;
            Tower newTower = Instantiate(towerPrefab, spot.transform).GetComponentInChildren<Tower>();
        }
        spot.ChangeToSelecting();
    }
}
