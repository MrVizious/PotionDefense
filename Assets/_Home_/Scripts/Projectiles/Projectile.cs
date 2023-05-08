using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using UltEvents;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Projectile : Poolable<Projectile>
{
    public float speed = 2f;
    public float damage = 5f;
    public float secondsToDie = 10f;

    private Coroutine dieAfterCoroutine = null;



    // Ejecuta cada frame
    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public override void Init(Pool<Projectile> newPool)
    {
        base.Init(newPool);
        gameObject.SetActive(true);
        dieAfterCoroutine = StartCoroutine(DieAfter(secondsToDie));
    }

    public override void OnPoolRelease()
    {
        base.OnPoolRelease();

        // Die after coroutine cleanup
        if (dieAfterCoroutine != null) StopCoroutine(dieAfterCoroutine);
        dieAfterCoroutine = null;

        foreach (ProjectileModifier modifier in GetComponents<ProjectileModifier>())
        {
            Destroy(modifier);
        }

        // Set to false
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null) return;
        enemy.Damage(damage);

        foreach (ProjectileModifier modifier in GetComponents<ProjectileModifier>())
        {
            modifier.OnHit(enemy);
        }
        Release();
    }

    private IEnumerator DieAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Release();
    }

}
