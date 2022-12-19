using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PLATFORMER.Combat;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    // public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Health enemy = other.GetComponent<Health>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
        // Instantiate(impactEffect, transform.position, transform.rotation);

        // Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
