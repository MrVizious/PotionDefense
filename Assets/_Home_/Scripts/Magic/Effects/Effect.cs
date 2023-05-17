using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public float durationInSeconds = -1f;
    public int level = 1;
    protected Enemy enemy;
    protected Coroutine endEffectCoroutine;
    protected void OnEnable()
    {
        enemy = GetComponent<Enemy>();
    }

    public virtual void Begin()
    {
        if (enemy == null) return;
        if (!enemy.isActiveAndEnabled) return;
        if (durationInSeconds > 0f)
        {
            endEffectCoroutine = StartCoroutine(EndEffectCountdown(durationInSeconds));
        }
    }

    public virtual void End()
    {
        if (endEffectCoroutine != null) StopCoroutine(endEffectCoroutine);
        endEffectCoroutine = null;

        Destroy(this);
    }

    protected IEnumerator EndEffectCountdown(float secondsToEndEffect)
    {
        yield return new WaitForSeconds(secondsToEndEffect);
        End();
    }

}