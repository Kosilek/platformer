using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPlayer : MonoBehaviour
{
    public GameObject player; 
    public void Awake()
    {
        //Instantiate(player, transform.position, transform.rotation);
        Instantiate(player);
    }
}
