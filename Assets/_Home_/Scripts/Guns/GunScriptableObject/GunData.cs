using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TypeReferences;

[CreateAssetMenu(fileName = "GunData", menuName = "Top-Down Shooter/GunData", order = 0)]
public class GunData : ScriptableObject
{
    [Header("Gun information")]
    public int magazineSize;
    public float damagePerHit, reloadSeconds;
    public bool hasCooldown;
    [ShowIf("hasCooldown")]
    public float cooldownSeconds;
    public bool hasCharging;
    [ShowIf("hasCharging")]
    public float chargeSeconds = 0f;

    [Header("Gun information")]
    [Inherits(typeof(GunReadyState), IncludeBaseType = true)]

    public TypeReference readyState;
    [Inherits(typeof(GunShootingState), IncludeBaseType = true)]

    public TypeReference shootingState;
    [Inherits(typeof(GunEmptyState), IncludeBaseType = true)]
    public TypeReference emptyState;
    [Inherits(typeof(GunReloadingState), IncludeBaseType = true)]

    public TypeReference reloadingState;
    [ShowIf("hasCharging")]
    [Inherits(typeof(GunChargingState), IncludeBaseType = true)]

    public TypeReference chargingState;
    [ShowIf("hasCooldown")]
    [Inherits(typeof(GunCooldownState), IncludeBaseType = true)]

    public TypeReference cooldownState;


}