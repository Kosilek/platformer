using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject finishText;

    private void Start()
    {
        finishText.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        finishText.SetActive(true);
        if (collision.tag == "Player")
            finishText.SetActive(true);
    }
}
