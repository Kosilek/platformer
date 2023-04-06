using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{


    public float timerStart = 0;
    public float timerFinishPlayer;




    private void Update()
    {
        timerStart += Time.deltaTime;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timerFinishPlayer = timerStart;
            Event.SendFinishTimer(timerFinishPlayer);
            Event.SendFinish();
        }
    }
}

