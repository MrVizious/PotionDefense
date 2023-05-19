using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWheel : MonoBehaviour
{
    public GameObject wheelPrefab;
    public float angleOffset = 5f;
    public List<OptionsWheelActions> actions;


    private int numberOfSectors;
    private float angleWidthTotalPerSector, angleCenterPerSector;
    private Tower tower;

    private void Start()
    {
        CalculateAngles();
        SpawnSectors();
    }

    private void CalculateAngles()
    {
        numberOfSectors = actions.Count;
        if (numberOfSectors < 2) return;
        angleCenterPerSector = 360f / numberOfSectors;
        angleWidthTotalPerSector = angleCenterPerSector - angleOffset;
    }

    private void SpawnSectors()
    {
        int i = 0;
        foreach (OptionsWheelActions action in actions)
        {
            GameObject sector = Instantiate(wheelPrefab, transform);
            sector.name = "Sector_" + i;
            Image image = sector.GetComponentInChildren<Image>();
            image.fillAmount = angleWidthTotalPerSector / 360f;
            sector.transform.rotation = Quaternion.Euler(0f, 0f,
                // Angle calculation for z axis
                angleCenterPerSector * i - angleCenterPerSector / 2 + angleOffset / 2);
            i++;
        }
    }


}
