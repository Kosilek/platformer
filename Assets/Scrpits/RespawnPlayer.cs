using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject[] button;
 //   public Transform spawnMob;
 //   public GameObject enemy;
    private void Start()
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
   //     Instantiate(enemy, spawnMob.position, spawnMob.rotation);
    }
    private void Update()
    {
        if (GameObject.Find("Player(Clone)") == null)
        {
            button[0].SetActive(true); button[1].SetActive(true);
        }
    }

    public void Restart()
    {

    }
}
