using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using JetBrains.Annotations;

public class levelMenager : MonoBehaviour
{
    public UnityEvent<int> hpEvent = new UnityEvent<int>();
    public UnityEvent<int> hpEnemyEvent = new UnityEvent<int>();
    public GameObject[] level;
    public GameObject player;
    public GameObject Score;
  //  public GameObject HpBarEnemy;
  //  public GameObject HpBar;
    // public GameObject Coin;
   // public int coins = 0;

    private void Start()
    {
        level[0].SetActive(true);
        level[1].SetActive(false);
        level[2].SetActive(false);
        Score.SetActive(false);
       // HpBarEnemy.SetActive(false);
       // HpBar.SetActive(false);
    }


    public void LevelOne()
    {
        level[0].SetActive(false);
        level[1].SetActive(true);
        level[2].SetActive(false);
        Instantiate(player);
        Score.SetActive(true);
      //  HpBar.SetActive(true);

    }

    public void LevelTwo()
    {
        level[0].SetActive(false);
        level[1].SetActive(false);
        level[2].SetActive(true);
        Instantiate (player);
        Score.SetActive(true);
       // HpBar.SetActive(true);
    }


}
