using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float lifetime = 6f;
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        waypointScript enemy = collision.gameObject.GetComponent<waypointScript>();

        if (enemy != null)
        {
            enemy.TakeDamage();
        }

        if (collision.gameObject.GetComponent<Car>() == null)
            Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        this.transform.position += transform.rotation * Vector2.up * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            Destroy(this.gameObject);
    }

    public virtual void Fire(Transform weaponLeft, Transform weaponRight)
    {
        Instantiate(this, weaponRight);
        Instantiate(this, weaponLeft);
    }
}
