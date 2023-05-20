using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

public static class IconReferences
{
    public static Dictionary<TypeReference, Sprite> optionWheelActionSprites =
    new Dictionary<TypeReference, Sprite> {
        {typeof(SlowModifier), Resources.Load<Sprite>("Art/Sprites/UI/snowflake")},
        {typeof(SellWheelAction), Resources.Load<Sprite>("Art/Sprites/UI/snowflake")}
    };
}
