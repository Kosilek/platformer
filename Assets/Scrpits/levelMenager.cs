using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class levelMenager : MonoBehaviour
{
    public GameObject player;
    // public GameObject[] dieButton;
    // public GameObject Score;
    public GameObject[] levelOnject;
    public Transform spawn;
    private int level;

    private void Awake()
    {
        DeactivityLevel();
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
        levelOnject[level].SetActive(true);
        Instantiate(player, spawn.position, spawn.rotation);
    }


    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void DeactivityLevel()
    {
        levelOnject[0].SetActive(false);
        levelOnject[1].SetActive(false);
    }
}

   // public bool q = false;
  //  public GameObject HpBarEnemy;
  //  public GameObject HpBar;
    // public GameObject Coin;
   // public int coins = 0;

 //   private void Awake()
  ////  {
      //  level[0].SetActive(true);
      ////  level[1].SetActive(false);
      //  level[2].SetActive(false);
      //  Score.SetActive(false);
      //  dieButton[0].SetActive(false);
      //  dieButton[1].SetActive(!false);
       // HpBarEnemy.SetActive(false);
       // HpBar.SetActive(false);  
   /* public void LevelOne()
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
    }*/
   /* public void Restart()
    {
        level[0].SetActive(false);
        level[1].SetActive(false);
        level[2].SetActive(false);
        Score.SetActive(false);
        int checklevel = 0;
        checklevel = CheckLevel();
        switch (checklevel)
        {
            case 1:
                level[0].SetActive(false);
                level[1].SetActive(true);
                level[2].SetActive(false);
                Score.SetActive(true);
                break;
            case 2:
                level[0].SetActive(false);
                level[1].SetActive(false);
                level[2].SetActive(true);
                Score.SetActive(true);
                break;
        }
    }
    
    private int CheckLevel()
    {
        int level = 0;
        if (GameObject.Find("level1"))
        {
            level = 1;
        } else if (GameObject.Find("level2"))
        {
            level = 2;
        }
        return level;
    }

    public void Menu()
    {
        
        level[1].SetActive(false);
        level[2].SetActive(false);
        Score.SetActive(false);
        level[0].SetActive(true);
    }*/

