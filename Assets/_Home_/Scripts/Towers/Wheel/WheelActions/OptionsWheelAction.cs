using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UtilityMethods;
using ExtensionMethods;

public abstract class OptionsWheelAction : MonoBehaviour
{
    protected Image icon;
    protected Image sector;
    protected virtual string iconRoute
    {
        get => "";
    }
    protected virtual void Start()
    {
        sector = GetComponentInChildren<Image>();
        SetIcon();
    }

    protected virtual void SetIcon()
    {
        GameObject iconGO = new GameObject("Icon");
        icon = iconGO.AddComponent<NonRaycastableTransparencyImage>();
        icon.sprite = Resources.Load<Sprite>(iconRoute);
        icon.raycastTarget = false;
        RectTransform rectTransform = iconGO.GetComponent<RectTransform>();
        iconGO.transform.SetParent(GetComponentInChildren<Canvas>().transform);
        rectTransform.sizeDelta = Vector2.one;
        rectTransform.localPosition = Math.PolarToCartesianClockwise(0.25f, sector.fillAmount * 360 / 2).WithZ(-0.1f);
        iconGO.transform.localScale = Vector3.one * 0.25f;
        iconGO.transform.rotation = Quaternion.identity;
    }
    public abstract void Execute(TowerSpot spot);
}
