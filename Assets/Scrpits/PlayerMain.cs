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

  //  public UnityEvent<float> HpBarEvent = new UnityEvent<float>();

    public float speed;
    public float jumpForce;
    public bool isGrounder = false;
  //  public GameObject Ground;
    public Transform firePoint;
    public GameObject bullet;

 //   public Transform groundDetection;
  //  public float distance;
 //   public int hpBar = 100;
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
       // RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

       // if (groundInfo.collider == false)
       // {
       //     isGrounder = false;
       // }
       // else if (groundInfo.collider == true)
        //{
        //    isGrounder = true;
       // }

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
        //  if (Input.GetKeyDown(KeyCode.Escape))
        //  {
        //    SceneManager.LoadScene(0);
        //  }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounder = true;
       
        if (collision.gameObject.CompareTag("Enemy"))
        {
            sprite.material = matBlink;
            Invoke("ResetMaterial", .2f);
            Event.SendTakeDamage(collision.gameObject.GetComponent<Enemy>().DamagePlayer);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounder = false;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            Death();
        }
        if (collision.gameObject.tag == "Finish")
        {
            speed = 0;
            animator.SetInteger("State", 7);
            //     button[0].SetActive(true);
            //     button[1].SetActive(true);
            //      finishText.SetActive(true);
            //     scoreFinish.text = score.ToString();

        }
        Coins coins = collision.gameObject.GetComponent<Coins>();
        if (collision.CompareTag("Coin"))
        {
            coins.AddCoin();
         //   Destroy(collision.gameObject);
        }
    }

    private void Death()
    {
        Debug.Log("Âû óáèòû");
        animator.SetInteger("State", 9);
        speed = 0;
        //      button[0].SetActive(true);
        //      button[1].SetActive(true);
        Destroy(gameObject, 0.9f);
    }

    void ResetMaterial()
    {
        sprite.material = matDefault;
    }


        //    scoreText.text = score.ToString();

        //  }

        //  public void TakePlayerDamage(int damage)
        //  {
        //      Hp -= damage;
        //     HpBar.text = Hp.ToString();
        //     if (Hp <= 0)
        //     {
        //         Death();
        //    }
        void Shoot()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    
}
