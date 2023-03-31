using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCntr : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public bool isAttack;

    public float speed;
    public float jumpForce;

    public bool isGrounder = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;

    private SpriteRenderer rbSprite;
    private Animator animator;

    public int score;
    public Text scoreText;

    public GameObject[] button;

    public bool GameOver = true;

    private bool facingRight = true;


    public Text HpBar;
    public int Hp;

    private void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scoreText.text = score.ToString();
        HpBar.text = Hp.ToString();
    }

    private void FixedUpdate()
    {
        Run();
        isGrounder = Physics2D.OverlapCircle(groundCheck.position, groundRadius);
        animator.SetBool("Ground", isGrounder);
        animator.SetFloat("vSpeed", rb.velocity.y);
        if (!isGrounder) return;
        //   if (Input.GetKeyDown(KeyCode.Space) && isGrounder)
        //  {
        //      Jump();
        //  }

        /*   Vector3 position = transform.position;
           position.x += Input.GetAxis("Horizontal") * speed;
           transform.position = position;
           if (Input.GetAxis("Horizontal") != 0)
           {
               if (Input.GetAxis("Horizontal") < 0)
               {
                   {
                       //rbSprite.flipX = true;
                       Flip();
                   }
               }
               else if (Input.GetAxis("Horizontal") > 0)
               {// rbSprite.flipX = false;
                   Flip(); 
               }
               animator.SetInteger("State", 1);
           }
           else
           {
               animator.SetInteger("State", 0);
           }*/
    }

    private void Update()
    {
        if (isGrounder && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpForce));
        }

        //   if (Input.GetMouseButton(0))
        //   {
        //       Fire();
        //   }



        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            animator.SetBool("isAttack", isAttack);
            Shoot();
            isAttack = false;          // Ñäåëàòü âûõîä èç àíèìàöèè
            animator.SetBool("isAttack", isAttack);
        }




        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    //  private void Jump()
    //  {
    //   if (isGrounder)
    //  {
    //      isGrounder = false;
    //     rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    //     animator.SetInteger("State", 2);//ïåðåäåëàòü
    //  }
    //  }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //  if (collision.gameObject.tag == "Ground")
        //  {
        //  isGrounder |= true;
        //  }
        if (collision.gameObject.tag == "Enemy")
        {
            //Death();
            Enemy mob = collision.gameObject.GetComponent<Enemy>();
            if (mob != null)
            {
                TakePlayerDamage(mob.DamagePlayer);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            Death();
        }
        if (collision.gameObject.tag == "Finish")
        {
            GetComponent<PlayerCntr>().speed = 0;
            button[0].SetActive(true);
            button[1].SetActive(true);
        }
    }

    private void Death()
    {
        Debug.Log("Âû óáèòû");
        animator.SetInteger("State", 9);
        GetComponent<PlayerCntr>().speed = 0;
        button[0].SetActive(true);
        button[1].SetActive(true);
        Destroy(gameObject, 0.9f);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Run()
    {
        float move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));

        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    public void AddCoin(int count)
    {
        score += count;
        scoreText.text = score.ToString();
    }

    public void TakePlayerDamage(int damage)
    {
        Hp -= damage;
        HpBar.text = Hp.ToString();
        if (Hp <= 0)
        {
            Death();
        }
    }
    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
