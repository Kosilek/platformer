using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPlaer : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        
        player.transform.position = transform.position;

    }
}
