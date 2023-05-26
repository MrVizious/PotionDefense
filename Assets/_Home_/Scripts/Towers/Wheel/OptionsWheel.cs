using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TypeReferences;
using ExtensionMethods;

public class OptionsWheel : MonoBehaviour
{
    public WheelSector sectorPrefab;
    public float angleOffset = 5f;
    public List<WheelSector> sectors;

    private TowerSpot _towerSpot;
    private TowerSpot towerSpot
    {
        get
        {
            if (_towerSpot == null)
            {
                _towerSpot = transform.parent.GetComponentInChildren<TowerSpot>();
            }
            return _towerSpot;
        }
    }


    private int numberOfSectors
    {
        get => sectors.Count;
    }
    private float angleWidthTotalPerSector, angleCenterPerSector;
    private Tower tower;

    public void ClearActions()
    {
        foreach (WheelSector sector in sectors)
        {
            Destroy(sector.gameObject);
        }
        sectors.Clear();
    }

    public void AddAction(TypeReference actionTypeToAdd)
    {
        WheelSector newSector = Instantiate(sectorPrefab, transform);
        Component actionAdded = newSector.GetOrAddComponent(actionTypeToAdd);
        if (actionTypeToAdd.Type == typeof(EvolveWheelAction))
        {
            if (!towerSpot.tower.CanEvolve())
            {
                newSector.button.interactable = false;
            }
        }
        else if (actionTypeToAdd.Type == typeof(BuyFireTowerWheelAction))
        {
            float cost = ((BuyFireTowerWheelAction)actionAdded).towerPrefab.GetComponentInChildren<Tower>().data.cost;
            if (cost > FindObjectOfType<LevelManager>().experience)
            {
                newSector.button.interactable = false;
            }
        }
        else if (actionTypeToAdd.Type == typeof(BuyIceTowerWheelAction))
        {
            float cost = ((BuyIceTowerWheelAction)actionAdded).towerPrefab.GetComponentInChildren<Tower>().data.cost;
            if (cost > FindObjectOfType<LevelManager>().experience)
            {
                newSector.button.interactable = false;
            }
        }
        sectors.Add(newSector);
    }
    private void CalculateAngles()
    {
        if (numberOfSectors < 2) return;
        angleCenterPerSector = 360f / numberOfSectors;
        angleWidthTotalPerSector = angleCenterPerSector - angleOffset;
    }

    public void RenderSectors()
    {
        if (numberOfSectors <= 0) return;
        if (numberOfSectors == 1)
        {
            WheelSector sector = sectors[0];
            sector.gameObject.name = "Sector_0";
            sector.sectorImage.fillAmount = 1f;
            sector.transform.rotation = Quaternion.identity;
            sector.button.onClick.AddListener(
                () => sector.action.Execute(towerSpot)
            );
            return;
        }

        CalculateAngles();

        for (int i = 0; i < sectors.Count; i++)
        {
            WheelSector sector = sectors[i];
            sector.gameObject.name = "Sector_" + i;
            sector.sectorImage.fillAmount = angleWidthTotalPerSector / 360f;
            sector.transform.rotation = Quaternion.Euler(0f, 0f,
                // Angle calculation for z axis
                (angleCenterPerSector * i) - angleCenterPerSector / 2 - angleOffset / 2);
            sector.button.onClick.RemoveListener(
                () => sector.action.Execute(towerSpot)
            );
            sector.button.onClick.AddListener(
                () => sector.action.Execute(towerSpot)
            );
        }
    }
}