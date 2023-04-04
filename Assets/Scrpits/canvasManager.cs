using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{
    public Text scoreText;

    public int score;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void ScoreCoin(int coin)
    {

        score = score + coin;
        scoreText.text = score.ToString();

    }
}
