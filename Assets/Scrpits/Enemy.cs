using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer rbSpriteMob;
    private Material matBlink;
    private Material matDefault;
    public UnityEvent<float> HpBarEvent = new UnityEvent<float>();
    public float speed;
    public float distance;
    [SerializeField] private bool moovingRight = true;
    public float rayDistance;
    public float JumpFors;
    public bool isGrounderEnemy = false;
    public Transform groundCheckEnemy;
    float groundRadiusEnemy = 0.2f;
    Animator animator;
    private Rigidbody2D rb;
    public Transform groundDetection;
    public bool touchBlock;
    private int currentTarget;
    public float DamagePlayer = 20f;
    public GameObject player;

    public bool Test = false;
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        rbSpriteMob = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = rbSpriteMob.material;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (moovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                moovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moovingRight = true;
            }
        }
        if (moovingRight == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
            JumpAndTarger(hit);
        }
        else if (moovingRight == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayDistance);
            JumpAndTarger(hit);
        }  
    }
    private void JumpAndTarger(RaycastHit2D hit)
    {  
        if (hit)
        {
            if (hit.collider.name == "Player")
            {
                Test = true;
            }
            CheckBlock(hit, "block");
            CheckBlock(hit, "Ground");
        }
    }
    private void CheckBlock(RaycastHit2D hit ,string block)
    {
        if (hit.collider.tag == block)
        {
            rb.velocity = Vector2.up * JumpFors;
            Debug.Log("!!!!");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * rayDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundDetection.position, groundDetection.position + Vector3.down * distance);
    }
    public void FixedUpdate()
    {
        isGrounderEnemy = Physics2D.OverlapCircle(groundCheckEnemy.position, groundRadiusEnemy);
        if (!isGrounderEnemy) return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Death();
        }
        if (collision.CompareTag("bullet"))
        {
            rbSpriteMob.material = matBlink;
            Invoke("ResetMaterial", .2f);
            // Event.SendTakeDamage(collision.gameObject.GetComponent<Bullet2D>().damage);
            HpBarEvent.Invoke(collision.gameObject.GetComponent<Bullet2D>().damage);
        }
    }
   void ResetMaterial()
    {
        rbSpriteMob.material = matDefault;
    }
    private void Death()
    {
        animator.SetInteger("State", 9);
        GetComponent<Enemy>().speed = 0;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        Destroy(gameObject, 0.7f);
    }
}
