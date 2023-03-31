using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPlayer : MonoBehaviour
{
    public Transform StartPlayer;

    private void Start()
    {
        StartPlayer.transform.position = transform.position;
    }
}
