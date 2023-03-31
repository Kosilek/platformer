using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject[] button = new GameObject[1];
    private int index;
    private void Start()

    {
        button[0].SetActive(false);
        button[1].SetActive(false);
        index = SceneManager.GetActiveScene().buildIndex;
    }
    public void Restarting()
    {
        SceneManager.LoadScene(index);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}
