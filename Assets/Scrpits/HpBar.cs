using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    public float hp;
    public Animator anim;

    private void Start()
    {
        fill = 1f;
     //   bar.fillAmount = fill;
    }

    private void Update()
    {
        bar.fillAmount = fill;

        if (hp <= 0f)
        {
            Death();
        }
    }
    public void TakeDamage(float Damage)
    {
        hp -= Damage;
        fill = (hp / 100);
        
    }
    private void Death()
    {
        Debug.Log("Âû óáèòû");
        anim.SetInteger("State", 9);
     //   GetComponent<PlayerMain>().speed = 0;
        //      button[0].SetActive(true);
        //      button[1].SetActive(true);
        Destroy(gameObject, 0.9f);
    }

}
