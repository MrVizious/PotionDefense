using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    protected TowerData data;
    protected Enemy enemy;
    protected Coroutine endEffectCoroutine;
    protected void OnEnable()
    {
        enemy = GetComponent<Enemy>();
    }

    public virtual void Begin(TowerData newData)
    {
        data = newData;
        if (enemy == null) return;
        if (!enemy.isActiveAndEnabled) return;
        enemy.onDie += End;
        if (newData.effectDurationInSeconds > 0f)
        {
            endEffectCoroutine = StartCoroutine(EndEffectCountdown(newData.effectDurationInSeconds));
        }
    }

    public virtual void End()
    {
        if (endEffectCoroutine != null) StopCoroutine(endEffectCoroutine);
        endEffectCoroutine = null;
        StopAllCoroutines();
        enemy.onDie -= End;

        Destroy(this);
    }

    protected IEnumerator EndEffectCountdown(float secondsToEndEffect)
    {
        yield return new WaitForSeconds(secondsToEndEffect);
        End();
    }

    protected void OnDestroy()
    {
        End();
    }

}
