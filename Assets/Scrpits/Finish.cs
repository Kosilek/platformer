using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
   // public Text scoreText;
   // int score;

    public GameObject finishText;

    private void Start()
    {
        
        finishText.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //scoreText.text = score.ToString();
       // finishText.SetActive(true);
        if (collision.tag == "Player")
            finishText.SetActive(true);
    }
}

