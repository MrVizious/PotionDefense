using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class FollowingProjectile : Projectile
{

    public float turningSpeed;
    public Transform target;

    private Coroutine dieAfterCoroutine = null;


    // Ejecuta cada frame
    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (target == null) Release();
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(transform.forward, target.position - transform.position),
            turningSpeed * Time.deltaTime
        );
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public override void Shoot(Vector3 position, Quaternion rotation, int layer, Transform target = null)
    {
        if (target == null) Release();
        transform.position = position;
        transform.rotation = rotation;
        gameObject.layer = layer;
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

    public override void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable == null) return;
        damageable.Damage(damage);

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null) return;
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
