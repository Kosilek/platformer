using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // private Transform target;   Ïðèäóìàòü êàê îáüåäèíèòü ñ îñí. Äâèæåíèåì
    // public float stoppingDistance;   Ïðèäóìàòü êàê îáüåäèíèòü ñ îñí. Äâèæåíèåì
    private SpriteRenderer rbSpriteMob;
    private Material matBlink;
    private Material matDefault;

    // public float Jumping;
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
        
      // 
      //  SetHp = Hp;
      //  EnemyHp.gameObject.SetActive(false); //Ðàçîáðàòüñÿ êàê àêòèâèðîâàòü
        rbSpriteMob = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EnemyHp.text = Hp.ToString();
        enemyHp.SetActive(false);
        // gameObject.GetComponent<Text>().enabled = false;
        //canvas.gameObject.SetActive(false);
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();     Ïðèäóìàòü êàê îáüåäèíèòü ñ îñí. Äâèæåíèåì
   
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = rbSpriteMob.material;
    }

    private void Update()
    {
        //   if ( Vector2.Distance(transform.position, target.position) > stoppingDistance)  Ïðèäóìàòü êàê îáüåäèíèòü ñ îñí. Äâèæåíèåì
        //  transform.position = Vector2.MoveTowards(transform.position, target.position, speedMob * Time.deltaTime);   Ïðèäóìàòü êàê îáüåäèíèòü ñ îñí. Äâèæåíèåì
        //  if (SetHp != Hp)
        //     gameObject.GetComponent<Text>().enabled = true;

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

        /*    if (moovingRight == true)
           {
               RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
               if (hit.collider != null && isGrounderEnemy)
               {
                   rb.AddForce(new Vector2(0, 100));
               }
           }
            else if (moovingRight == false && isGrounderEnemy)
           {
               RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
               if (hit.collider != null)
               {
                   rb.AddForce(new Vector2(0, 100));
               }
           }*/


    }
    public void FixedUpdate()// Ïåðåäåëàòü 
    {
        isGrounderEnemy = Physics2D.OverlapCircle(groundCheckEnemy.position, groundRadiusEnemy);
        //animator.SetBool("Ground", isGrounder);
        // animator.SetFloat("vSpeed", rb.velocity.y);
        if (!isGrounderEnemy) return;








        /* transform.position = Vector3.MoveTowards(transform.position, positions[currentTarget], speedMob);
         if (transform.position == positions[currentTarget])
         {
             if (currentTarget < positions.Length - 1)
             {
                 currentTarget++;
             }
             else
             {
                 currentTarget = 0;
             }
         }
         if (touchBlock)
         {
             rbSpriteMob.flipX = false;
         }
         else
         {
             rbSpriteMob.flipX = true;
         }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*   if (collision.gameObject.tag == "blockL")       Ïåðåäåëàòü ïåðåïðûãèâàíèå ïðåïÿòñâèé
           {
               rb.AddForce(new Vector2(speedMob * Time.deltaTime, 1000));
           }
           if (collision.gameObject.tag == "blockR")
           {
               rb.AddForce(new Vector2(speedMob * Time.deltaTime, 1000));
           }*/

        //  if (collision.gameObject.tag == "bullet")
        // {
        //    gameObject.GetComponent<Text>().enabled = true;
        // }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//Èñïðàâèòü êîñÿê ñ ñìåðòüþ
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
        //   if (collision.gameObject.tag == "bullet")
        //    {
        //        gameObject.GetComponent<Text>().enabled = true;
        //   }
        // if (collision.gameObject.tag == "bullet")
        //  {
        // Destroy(gameObject);
        //  }
    }

    void ResetMaterial()
    {
        rbSpriteMob.material = matDefault;
    }
    private void Death()
    {
     //   Debug.Log("Ìîá óáèò");
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
        //Hp -= damage;
        // gameObject.GetComponent<Text>().enabled = true;
        EnemyHp.text = Hp.ToString();
        if (Hp <= 0)
        {
            Death();
        }
    }
}
