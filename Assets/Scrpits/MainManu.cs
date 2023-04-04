using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public GameObject[] button;
 

 

    private void Start()
    {
        button[2].SetActive(false);
        button[3].SetActive(false);
    }
    public void Play()
    {
        button[2].SetActive(true);
        button[3].SetActive(true);
        button[0].SetActive(false);
        button[1].SetActive(false);
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
