using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class Event
{
    public static UnityEvent<int> OnScoreCoin = new UnityEvent<int>();
    public static UnityEvent<float> OnTakeDamage = new UnityEvent<float>();

    public static void SendScoreCoin(int score)
    {
        OnScoreCoin.Invoke(score);
    }

    public static void SendTakeDamage(float damage)
    {
        OnTakeDamage.Invoke(damage);
    }
}
