using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class Event
{
    public static UnityEvent<int> OnScoreCoin = new UnityEvent<int>();
    public static UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public static UnityEvent<float> OnFinishTimer = new UnityEvent<float>();
    public static UnityEvent OnFinish = new UnityEvent();
    public static UnityEvent OnScore = new UnityEvent();
    public static UnityEvent OnTimer = new UnityEvent();


    public static void SendScoreCoin(int score)
    {
        OnScoreCoin.Invoke(score);
    }

    public static void SendTakeDamage(float damage)
    {
        OnTakeDamage.Invoke(damage);
    }

    public static void SendFinishTimer(float timer)
    {
        OnFinishTimer.Invoke(timer);
    }

    public static void SendFinish()
    {
        OnFinish.Invoke();
    }

    public static void SendScore()
    {
        OnScore.Invoke();
    }

    public static void SendTimer()
    {
        OnTimer.Invoke();
    }
}
