using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{
    public Text scoreText;

    public int score = 0;


    private void Awake()
    {
        scoreText.text = score.ToString();
        Event.OnScoreCoin.AddListener(ScoreCoin);
    }

    public void ScoreCoin(int coin)
    {

        score = score + coin;
        scoreText.text = score.ToString();

    }


}
