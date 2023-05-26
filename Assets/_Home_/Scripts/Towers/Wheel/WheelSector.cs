using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelSector : MonoBehaviour
{
    public Image icon
    {
        get => action?.icon;
    }
    public Image sectorImage;
    public Button button;
    public OptionsWheelAction action
    {
        get
        {
            if (_action == null)
            {
                _action = GetComponent<OptionsWheelAction>();
            }
            return _action;
        }
    }
    private OptionsWheelAction _action;
}