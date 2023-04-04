using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer rbSpriteMob;
    private Material matBlink;
    private Material matDefault;

    public UnityEvent<float> HpBarEvent = new UnityEvent<float>();

    public float speed;
    public float distance;
    private bool moovingRight;
    public float rayDistance;
    public bool isGrounderEnemy = false;
    public Transform groundCheckEnemy;
    float groundRadiusEnemy = 0.2f;

    Animator animator;
    private Rigidbody2D rb;

    public Transform groundDetection;

    public bool touchBlock;
    private int currentTarget;

    //  public Text EnemyHp;
    //public float Hp;
    public float DamagePlayer = 20f;
    // public GameObject enemyHp;
    private void Start()
    {
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
    }
    public void FixedUpdate()
    {
        isGrounderEnemy = Physics2D.OverlapCircle(groundCheckEnemy.position, groundRadiusEnemy);
        if (!isGrounderEnemy) return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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

    /*    public void TakeDamage(int damage)
        {
            Hp -= damage;
            enemyHp.SetActive(true);
            EnemyHp.text = Hp.ToString();
            if (Hp <= 0)
            {
                Death();
            }
        }
    }*/
}
