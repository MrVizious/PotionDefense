using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ExtensionMethods;
using UtilityMethods;
using Cysharp.Threading.Tasks;

public class ElectricityEffect : Effect
{
    public override void Begin(TowerData newData)
    {
        base.Begin(newData);
        if (enemy == null) return;
        UniTaskMethods.DelayedFunction(() => ElectrifyCloseEnemies(), 0.2f).Forget();
    }

    private void ElectrifyCloseEnemies()
    {
        List<Effect> currentEffects = new List<Effect>(GetComponents<Effect>());
        currentEffects.Remove(this);
        Debug.Log("Current effects count " + currentEffects.Count);
        Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, data.range);
        foreach (Collider2D collider in closeColliders)
        {
            Enemy currentEnemy = collider.GetComponent<Enemy>();
            if (currentEnemy == null) continue;
            if (currentEnemy.HasComponent<ElectricityEffect>()) continue;
            Debug.Log("Checking " + currentEnemy, currentEnemy);
            foreach (Effect effect in currentEffects)
            {
                Debug.Log("Applying effect " + effect.GetType() + " to " + currentEnemy);
                Effect newEffect = (Effect)currentEnemy.gameObject.GetOrAddComponent(effect.GetType());
                newEffect.Begin(effect.data);
            }
            ((ElectricityEffect)currentEnemy.GetOrAddComponent(this.GetType())).Begin(data);
        }
    }

}
