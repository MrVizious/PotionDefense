using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class Projectile : Poolable<Projectile>
{
    public float speed = 2f;
    public float damage = 5f;

    // Ejecuta cada frame
    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public override void Init(Pool<Projectile> newPool)
    {
        base.Init(newPool);
        gameObject.SetActive(true);
    }

    public override void OnPoolRelease()
    {
        base.OnPoolRelease();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit something!");
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy == null) return;
        enemy.Hurt(damage);
        Release();
    }

}
