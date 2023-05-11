using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using Sirenix.OdinInspector;

public class SimpleProjectile : Poolable, IProjectile
{
    [SerializeField]
    private float _speed;
    public float speed => _speed;
    public float speedModifier { get; private set; }
    [SerializeField]
    private float _damage;
    public float damage => _damage;
    public float damageModifier { get; private set; }
    [SerializeField]
    public float _secondsToDie;
    public float secondsToDie => _secondsToDie;


    private Coroutine dieAfterCoroutine = null;


    // Ejecuta cada frame
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public void Shoot(Vector3 position, Quaternion rotation, int layer, Transform target = null)
    {
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
