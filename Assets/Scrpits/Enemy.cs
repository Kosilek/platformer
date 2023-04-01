using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer rbSpriteMob;
    private Material matBlink;
    private Material matDefault;

    public float speedMob;
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

    public Text EnemyHp;
    public int Hp;
    public int DamagePlayer;
    public GameObject enemyHp;
    private void Start()
    {
        rbSpriteMob = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EnemyHp.text = Hp.ToString();
        enemyHp.SetActive(false); 
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = rbSpriteMob.material;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speedMob * Time.deltaTime);

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
        if(collision.CompareTag("bullet"))
        {
            rbSpriteMob.material = matBlink;
            if (Hp > 0)
            {
                Invoke("ResetMaterial", .2f);
            }
        }
    }

    void ResetMaterial()
    {
        rbSpriteMob.material = matDefault;
    }
    private void Death()
    {
        animator.SetInteger("enemyState", 1);
        GetComponent<Enemy>().speedMob = 0;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        enemyHp.SetActive(false);
        Destroy(gameObject, 0.7f);
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        enemyHp.SetActive(true);
        EnemyHp.text = Hp.ToString();
        if (Hp <= 0)
        {
            Death();
        }
    }
}
