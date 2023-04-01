using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    // public Text scoreText;
    // int score;
    public float timerStart = 0;
    public Text timerFinish;
    public float timerFinishPlayer;
    // public GameObject finishText;

    private void Start()
    {
        timerFinish.text = timerStart.ToString();
    }

    private void Update()
    {
        timerStart += Time.deltaTime;
       // timerFinish.text = Mathf.Round(timerStart).ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //scoreText.text = score.ToString();
        // finishText.SetActive(true);
        //  if (collision.tag == "Player")
        //      finishText.SetActive(true);
        timerFinishPlayer = timerStart;
        timerFinish.text = Mathf.Round(timerFinishPlayer).ToString();
    }
}

