using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TypeReferences;
using ExtensionMethods;

public class OptionsWheel : MonoBehaviour
{
    public GameObject sectorPrefab;
    public float angleOffset = 5f;
    public List<GameObject> actionGameObjects;

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
        get => actionGameObjects.Count;
    }
    private float angleWidthTotalPerSector, angleCenterPerSector;
    private Tower tower;

    private void Start()
    {
        if (numberOfSectors > 1)
        {
            RenderSectors();
        }
    }

    public void ClearActions()
    {
        foreach (GameObject actionGO in actionGameObjects)
        {
            Destroy(actionGO);
        }
        actionGameObjects.Clear();
    }

    public void AddAction(TypeReference actionTypeToAdd)
    {
        GameObject newGO = Instantiate(sectorPrefab, transform);
        newGO.GetOrAddComponent(actionTypeToAdd);
        actionGameObjects.Add(newGO);
        RenderSectors();
    }
    private void CalculateAngles()
    {
        if (numberOfSectors < 2) return;
        angleCenterPerSector = 360f / numberOfSectors;
        angleWidthTotalPerSector = angleCenterPerSector - angleOffset;
    }


    private void RenderSectors()
    {
        CalculateAngles();
        int i = 0;
        foreach (GameObject actionGO in actionGameObjects)
        {
            actionGO.name = "Sector_" + i;
            Image image = actionGO.GetComponentInChildren<Image>();
            image.fillAmount = angleWidthTotalPerSector / 360f;
            actionGO.transform.rotation = Quaternion.Euler(0f, 0f,
                // Angle calculation for z axis
                (angleCenterPerSector * i) - angleCenterPerSector / 2 - angleOffset / 2);
            Debug.Log(actionGO.GetComponent<OptionsWheelAction>());
            actionGO.GetComponentInChildren<Button>().onClick.AddListener(
                () => actionGO.GetComponentInChildren<OptionsWheelAction>()
                              .Execute(towerSpot)

            );
            i++;
        }
    }
}