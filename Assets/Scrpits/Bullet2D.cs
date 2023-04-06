using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float damage = 40f;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (other.tag != "Player")
            Destroy(gameObject);
    }
}
