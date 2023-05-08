using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using ExtensionMethods;
using System;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

public class GunController : StateMachine<GunState>
{
    private GunInstance _currentGun;
    public GunInstance currentGun
    {
        get
        {
            if (_currentGun == null)
            {
                if (acquiredGuns != null && acquiredGuns.Count > 0)
                {
                    currentGun = acquiredGuns[0];
                    return _currentGun;
                }
                return null;
            }
            return _currentGun;
        }
        set
        {
            _currentGun = value;
        }
    }

    public GunReadyState currentGunReadyState => (GunReadyState)this.GetOrAddComponent(currentGun.gunData.readyState);
    public GunChargingState currentGunChargingState => (GunChargingState)this.GetOrAddComponent(currentGun.gunData.chargingState);
    public GunCooldownState currentGunCooldownState => (GunCooldownState)this.GetOrAddComponent(currentGun.gunData.cooldownState);
    public GunEmptyState currentGunEmptyState => (GunEmptyState)this.GetOrAddComponent(currentGun.gunData.emptyState);
    public GunReloadingState currentGunReloadingState => (GunReloadingState)this.GetOrAddComponent(currentGun.gunData.reloadingState);
    public GunShootingState currentGunShootingState => (GunShootingState)this.GetOrAddComponent(currentGun.gunData.shootingState);

    [Button]
    public void ClearStates()
    {
        currentState = null;
        stateStack.Clear();
        foreach (GunState state in transform.GetComponents<GunState>())
        {
            Destroy(state);
        }

    }
    [SerializeField] private List<GunInstance> acquiredGuns;
    private void Start()
    {
        PrepareCurrentGun();
    }
    private void PrepareCurrentGun()
    {
        ChangeToState(currentGunReadyState);
    }

    #region Gun Selection

    public void AddGun(GunData newGun, int currentBullets = -1, int extraBullets = -1)
    {
        acquiredGuns.Add(new GunInstance(newGun, currentBullets, extraBullets));
    }

    public void AddGun(GunInstance newGun)
    {
        acquiredGuns.Add(newGun);
    }

    [Button]
    public void RemoveCurrentGun()
    {
        ClearStates();
        GunInstance gunToRemove = currentGun;
        currentGun = acquiredGuns[(acquiredGuns.IndexOf(currentGun) + 1) % acquiredGuns.Count];
        acquiredGuns.Remove(gunToRemove);
    }

    private void ChangeGunTowards(int stepsToTake)
    {
        ClearStates();
        if (acquiredGuns.Count <= 1) return;
        currentGun = acquiredGuns[(acquiredGuns.IndexOf(currentGun) + stepsToTake) % acquiredGuns.Count];
        PrepareCurrentGun();
    }

    public void NextGun()
    {
        ChangeGunTowards(1);
    }
    public void PreviousGun()
    {
        ChangeGunTowards(-1);
    }

    #endregion

    #region Gun Actions

    public void Shoot(InputAction.CallbackContext c)
    {
        currentState?.Shoot(c);
    }

    public void Reload(InputAction.CallbackContext c)
    {
        if (c.performed)
            currentState?.Reload(c);
    }

    #endregion
}