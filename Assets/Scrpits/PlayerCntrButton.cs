using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerCntrButton : MonoBehaviour
{
    public float horizontalSpeed;
    private float speed;
    public float verticalImpulse;
    public bool isGrounder;
    private bool facingRight = true;
    public int Hp;
    public float timerAttack;
    public bool isAttack;
    public int score;
    public bool GameOver = true;

    Rigidbody2D rb;
    public Animator anim;
    public Text scoreText;
    public Transform firePoint;
    public GameObject bullet;
    public GameObject finishText;
    public Text scoreFinish;
    public GameObject[] button;
    public Text HpBar;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (isGrounder == false)
        {
             anim.SetBool("isGrounder", isGrounder);
           // if (isGrounder == true)
           // {
           //     anim.SetBool("isGrounder", isGrounder);
                //    anim.SetInteger("State", 3);
         //   }
        }
        anim.SetBool("isGrounder", isGrounder);
    }

    private void FixedUpdate()
    {
        transform.Translate(speed, 0, 0);
    }

    

    public void LeftButtonDown()
    {
        float move = -1;
        Move(move);
    }

    public void RightButtonDown()
    {
        float move = 1;
        Move(move);
    }

    public void Stor()
    {
        anim.SetInteger("State", 0);
        speed = 0;
    }

    private void Move(float move)
    {
        anim.SetInteger("State", 1);
        speed = 1 * horizontalSpeed * Time.deltaTime;  
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

    public void OnClickJump()
    {
        if (isGrounder)
            rb.AddForce(new Vector2(0, verticalImpulse), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounder = true;

        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounder = false;
    }
    
}
