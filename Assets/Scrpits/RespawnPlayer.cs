using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public bool q;
    public GameObject player;
    private void Update()
    {
        if (GameObject.Find("Player(Clone)") == null)
        {
            q = false;
            Instantiate(player);
        }
        else if (GameObject.Find("Player(Clone)") != null)
        {
            q = true;
        }
    }
}
