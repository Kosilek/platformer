using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCntr : MonoBehaviour
{
    
    public bool isAttack;
    public float speed;
    public float jumpForce;
    public int score;
    public bool isGrounder = false;
    public bool GameOver = true;
    private bool facingRight = true;
    public float timerAttack;
    public int Hp;
    private Rigidbody2D rb;
    private Animator animator;   
    public Text scoreText;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject finishText;
    public Text scoreFinish;
    public GameObject[] button;
    public Text HpBar;

    private void Start()
    {
        finishText.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scoreText.text = score.ToString();
        scoreFinish.text = score.ToString();
        HpBar.text = Hp.ToString();
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetInteger("State", 1);
            Run();
        }
        else animator.SetInteger("State", 0);

        //animator.SetBool("Ground", isGrounder);
       // animator.SetFloat("vSpeed", rb.velocity.y);
    }
    public void Run()
    {
        float move = Input.GetAxis("Horizontal");
       // animator.SetFloat("Speed", Mathf.Abs(move));

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

        if (isGrounder && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpForce));
        }
        animator.SetBool("Ground", isGrounder);
      
        if (Input.GetMouseButtonDown(0))
        {
            timerAttack = 0f;
            isAttack = true;
            animator.SetBool("isAttack", isAttack);
            timerAttack += Time.deltaTime;
            Shoot();
           // if (Input.GetMouseButtonUp(0))
           // isAttack = false;          // Ñäåëàòü âûõîä èç àíèìàöèè
           // animator.SetBool("isAttack", isAttack);
        }
        else if (Input.GetMouseButtonUp(0)) {
            isAttack = false;         
            animator.SetBool("isAttack", isAttack);}
            if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
          if (collision.gameObject.tag == "Ground")
          isGrounder = true;
          if (collision.gameObject.tag == "Enemy")
          {
              Enemy mob = collision.gameObject.GetComponent<Enemy>();
              if (mob != null)
              TakePlayerDamage(mob.DamagePlayer);
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
            GetComponent<PlayerCntr>().speed = 0;
            animator.SetInteger("State", 7);
            button[0].SetActive(true);
            button[1].SetActive(true);
            finishText.SetActive(true);
            scoreFinish.text = score.ToString();
         
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
