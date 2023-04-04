using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Coins : MonoBehaviour
{
   // public UnityEvent<int> coinAddEvent = new UnityEvent<int>();
    public int count = 5;


    public void AddCoin()
    {
        Event.SendScoreCoin(count);
        Destroy(gameObject);
///////////
    }
}
