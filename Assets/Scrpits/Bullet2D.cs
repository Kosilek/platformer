using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int damage;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (other.tag == "Enemy")
        {

            enemy.TakeDamage(damage);
        }
        if (other.tag != "Player")
            Destroy(gameObject);

        // if (other.tag != "Enemy")
        //   Destroy(gameObject);
    }
}