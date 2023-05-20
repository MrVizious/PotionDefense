using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStaticData : MonoBehaviour
{
    void Start()
    {
        GameObject newGO = new GameObject("Test", typeof(SpriteRenderer));
        Sprite sp = Resources.Load<Sprite>("Art/Sprites/UI/snowflake");

        //Sprite sp = IconReferences.optionWheelActionSprites[typeof(SellWheelAction)];
        newGO.GetComponent<SpriteRenderer>().sprite = sp;
    }


}
