using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class PredictiveProjectile : Projectile
{

    public float turningSpeed;

    // The distance at which the projectile starts aiming to hit, instead of predicting
    public float seekToHitDistance;
    // How much the projectile tries to predict where the target will be
    public float predictiveMagnitude;


    public Transform target;

    private Coroutine dieAfterCoroutine = null;
    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = target.GetComponent<Rigidbody2D>();
            }
            return _rb;
        }

    }


    // Ejecuta cada frame
    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        if (target == null) Release();
        Vector3 targetPosition;
        if (Vector2.Distance(transform.position, target.position) < seekToHitDistance)
        {
            targetPosition = target.position;
        }
        else
        {
            targetPosition = (Vector2)target.position + rb.velocity * predictiveMagnitude;
        }
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(transform.forward, targetPosition - transform.position),
            turningSpeed * Time.deltaTime
        );
        Debug.DrawLine(transform.position, targetPosition, Color.cyan);
        transform.position += transform.up * projectileData.speed * speedModifier * Time.deltaTime;
    }

    public override void Shoot(Vector3 position, Quaternion rotation, int layer, Transform target = null)
    {
        if (target == null) Release();
        transform.position = position;
        transform.rotation = rotation;
        gameObject.layer = layer;
        gameObject.SetActive(true);
        this.target = target;
        dieAfterCoroutine = StartCoroutine(DieAfter(projectileData.secondsToDie));
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
        damageable.Damage(projectileData.damage * damageModifier);

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            foreach (ProjectileModifier modifier in GetComponents<ProjectileModifier>())
            {
                modifier.OnHit(enemy);
            }
        }

        Release();
    }

    private IEnumerator DieAfter(float seconds)
    {
        if (seconds <= 0)
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(seconds);
            Release();
        }
    }


}
