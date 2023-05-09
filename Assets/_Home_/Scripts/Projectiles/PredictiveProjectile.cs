using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class PredictiveProjectile : Poolable<PredictiveProjectile>, IProjectile
{

    [SerializeField]
    private float _speed;
    public float speed => _speed;
    public float speedModifier { get; private set; }
    public float turningSpeed;
    private float _damage;
    public float damage => _damage;
    public float damageModifier { get; private set; }
    public float _secondsToDie;
    public float secondsToDie => _secondsToDie;

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

    public void Move()
    {
        if (target == null) Release();
        Vector3 targetPosition;
        Debug.Log(rb.velocity);
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
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public void Shoot(Vector3 position, Quaternion rotation, int layer, Transform target = null)
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

    public void OnCollisionEnter2D(Collision2D other)
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
