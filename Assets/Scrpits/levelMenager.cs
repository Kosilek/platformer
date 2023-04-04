using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelMenager : MonoBehaviour
{
    public GameObject[] level;
    public GameObject player;
    public GameObject Score;
    public GameObject HpBarEnemy;
   // public GameObject Coin;

   // public int coins = 0;

    private void Awake()
    {
        level[0].SetActive(true);
        level[1].SetActive(false);
        level[2].SetActive(false);
        Score.SetActive(false);
        HpBarEnemy.SetActive(false);
    }

    public void LevelOne()
    {
        level[0].SetActive(false);
        level[1].SetActive(true);
        level[2].SetActive(false);
        Instantiate(player);
        Score.SetActive(true);
    }

    public void LevelTwo()
    {
        level[0].SetActive(false);
        level[1].SetActive(false);
        level[2].SetActive(true);
        Instantiate (player);
        Score.SetActive(true);
    }

    
    //private void OnTriggerEnter2D(Collider2D collision)
   // {
   //     if (player == Coin)
   //         AddCoin(Coin.GetComponent<Coins>().count);
   //     Destroy(Coin);
//    }

   /* public void AddCoin(int count)
      {
        coins += count;
          //qqqqqqqqq
      }*/
}
