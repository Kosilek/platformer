using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Coins : MonoBehaviour
{
    public UnityEvent<int> coinAddEvent = new UnityEvent<int>();
    public int count = 5;
  //  public int score;
    /* private void OnTriggerEnter2D(Collider2D collision)
     {

         if(collision.CompareTag("Player"))
         {
             GetComponent<levelMenager>().AddCoin(count);
             Destroy(gameObject);
         }
     }*/

    public void AddCoin()
    {
        coinAddEvent.Invoke(count);//Попросить помощь...
        Destroy(gameObject);

    }
}
