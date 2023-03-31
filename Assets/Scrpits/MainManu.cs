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

    public void Level1()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
