using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using System.Drawing;

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
    private Transform player;
    public float stopDistance;
    public float speedAngry;
    public bool isGrounder;
    // public Transform point;
    public bool Test = false;

    private string difficulty;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficulty = PlayerPrefs.GetString("Difficulty");
        }
       
        Physics2D.queriesStartInColliders = false;
        rbSpriteMob = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = rbSpriteMob.material;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        animator.SetBool("Ground", isGrounder);
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, distance);
        if (groundInfo.collider != null)
        {
            isGrounder = true;
        }
        else if (groundInfo.collider == null) isGrounder = false;
        switch (difficulty)
        {
            case "easy":
                EasyLevel();
                break;
            case "normal":
                NormalLevel();
                break;
            case "hard":
                HardLevel();
                break;

        }
    }
    private void EasyLevel()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
        if (hit || groundInfo == false)
        {
            if (groundInfo.collider == false || hit.collider.tag == "block" || hit.collider.tag == "Ground")
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
    }

    private void NormalLevel()// доделать развороты
    {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);  // Доделать. Ошибки: Падает с обрыва если рядом игрок
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
          
            if (groundInfo == true && (hit.collider == null || hit.collider.tag != "block" || hit.collider.tag != "Ground"))
            {
                if (Vector2.Distance(transform.position, player.position) > stopDistance)
                {

                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.position, player.position) < stopDistance)
                {
                    if (transform.position.x > player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        moovingRight = false;
                        Angry();
                    }
                    else if (transform.position.x < player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        moovingRight = true;
                        Angry();
                    }
                }
            }
            else if (groundInfo == false || hit.collider.tag == "block" || hit.collider.tag == "Ground")
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
        else if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            EasyLevel();
        }
    }

    private void HardLevel()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);  // Доделать. Ошибки: Падает с обрыва если рядом игрок
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (groundInfo == true)
            {
                if (Vector2.Distance(transform.position, player.position) > stopDistance)
                {

                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.position, player.position) < stopDistance)
                {
                    if (transform.position.x > player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        moovingRight = false;
                        Angry();
                    }
                    else if (transform.position.x < player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        moovingRight = true;
                        Angry();
                    }
                }

            }
            else if (groundInfo == false)
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
        } else if (GameObject.FindGameObjectWithTag("Player") == null)
            {
            EasyLevel();
            }
            }

    private void JumpAndTarger(RaycastHit2D hit)
    {  
        if (hit)
        {
            CheckBlock(hit, "block");
            CheckBlock(hit, "Ground");
        }
    }

    private void Angry()
    {
        Debug.Log("Attack");
        transform.position = Vector2.MoveTowards(transform.position, player.position, speedAngry * Time.deltaTime);
    }
 
    private void CheckBlock(RaycastHit2D hit ,string block)
    {
        if (hit.collider.tag == block)
        {
            rb.velocity = Vector2.up * JumpFors;
            animator.SetBool("Ground", isGrounder);
            Debug.Log("!!!!");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * rayDistance);

        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance);

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
