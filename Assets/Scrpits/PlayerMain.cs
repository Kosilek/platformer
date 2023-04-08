using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMain : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Material matBlink;
    private Material matDefault;
    private Rigidbody2D rb;
    private Animator animator;
    private bool facingRight = true;
    private bool isAttack;
    public UnityEvent<float> HpBarEvent = new UnityEvent<float>();
    public float speed;
    public float jumpForce;
    public bool isGrounder = false;
    public Transform firePoint;
    public GameObject bullet;
    public float distance;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
          sprite = GetComponent<SpriteRenderer>();
         matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
           matDefault = sprite.material;
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetInteger("State", 1);
            Run();
        }
        else animator.SetInteger("State", 0);
    }

    public void Run()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, distance);
        if (groundInfo.collider != null)
        {
            isGrounder = true;
        } else if (groundInfo.collider == null) isGrounder = false;
        if (isGrounder && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpForce));
        }
        animator.SetBool("Ground", isGrounder);

        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            animator.SetBool("isAttack", isAttack);
            Shoot();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            isAttack = false;
            animator.SetBool("isAttack", isAttack);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

       
        if (collision.gameObject.CompareTag("Enemy"))
        {
            sprite.material = matBlink;
            Invoke("ResetMaterial", .2f);
            // Event.SendTakeDamage(collision.gameObject.GetComponent<Enemy>().DamagePlayer);
            HpBarEvent.Invoke(collision.gameObject.GetComponent<Enemy>().DamagePlayer);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("trap"))
        {
            Death();
        }
        if (collision.CompareTag("Finish"))
        {
            speed = 0;
            animator.SetInteger("State", 7);
        }
        if (collision.CompareTag("Coin"))
        {
            collision.gameObject.GetComponent<Coins>().AddCoin();
        }
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.down * distance);
    }

        private void Death()
    {
        Debug.Log("Âû óáèòû");
        animator.SetInteger("State", 9);
        speed = 0;
        Destroy(gameObject, 0.9f);
    }

    void ResetMaterial()
    {
        sprite.material = matDefault;
    }

        void Shoot()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
}
