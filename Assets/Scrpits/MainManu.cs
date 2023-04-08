using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public GameObject[] button;
    public GameObject records;
    public Text recordsScoreText;
    public Text recordsTimeText;
    private int recordsScore;
    private float recordsTime;

    public Text button3Text;

    public GameObject conditionsLevel2;
    public GameObject recordsLevel2;
    public GameObject buttonDifficultyObject;


    private void Start()
    {
        if (PlayerPrefs.HasKey("SaveScore"))
        {
            recordsScore = PlayerPrefs.GetInt("SaveScore");
        }
        if (PlayerPrefs.HasKey("SaveTime"))
        {
            recordsTime = PlayerPrefs.GetFloat("SaveTime");
        }
        buttonDifficultyObject.SetActive(false);
        recordsScoreText.text = ("����: " + recordsScore);
        recordsTimeText.text = ("�����: " + recordsTime);
        records.SetActive(false);
        recordsLevel2.SetActive(false);
        conditionsLevel2.SetActive(false);
        button[2].SetActive(false);
        button[3].SetActive(false);
        
    }
    public void Play()
    {
        button[2].SetActive(true);
        button[3].SetActive(true);
        CheckCoinLevel2(recordsScore);
        records.SetActive(true);
        button[0].SetActive(false);
        button[1].SetActive(false);
    }

    public void Level1()
    {
        button[2].SetActive(false);
        button[3].SetActive(false);
        conditionsLevel2.SetActive(false);
        records.SetActive(false);
        PlayerPrefs.SetInt("Level", 0);
        buttonDifficultyObject.SetActive(true);
    }

    public void Level2() 
    {
        button[2].SetActive(false);
        button[3].SetActive(false);
        records.SetActive(false);
        conditionsLevel2.SetActive(false);
        PlayerPrefs.SetInt("Level", 1);
        buttonDifficultyObject.SetActive(true);
    }

    private void CheckCoinLevel2(int score)
    {
        if (score >= 19)
        {
            button3Text.color = Color.red;
            recordsLevel2.SetActive(true);
           // button[3].GetComponent<Text>().color = Color.red;
            button[3].GetComponent<Button>().interactable = true;
        } else if( score < 40)
        {
            button3Text.color = Color.white;
            conditionsLevel2.SetActive(true);
           // button[3].GetComponent<Text>().color = Color.green;
            button[3].GetComponent<Button>().interactable = false;
        }
    }
    
   // private void ChoiceDifficulty()
  //  {
   //     buttonDifficultyObject.SetActive(true);
        
  //  }

    public void EasyDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", "easy");
        SceneManager.LoadScene("level1");
    }

    public void NormalDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", "normal");
        SceneManager.LoadScene("level1");
    }

    public void HatdDifficulty()
    {
        PlayerPrefs.SetString("Difficulty", "hard");
        SceneManager.LoadScene("level1");
    }

 /*    public void LevelOne()
    {
        level[0].SetActive(false);
        level[1].SetActive(true);
        level[2].SetActive(false);
        Instantiate(player);
    }

   public void LevelTwo()
    {
        level[0].SetActive(false);
        level[1].SetActive(false);
        level[2].SetActive(true);
        Instantiate(player);
    }*/

    public void Exit()
    {
        Application.Quit();
    }
}
