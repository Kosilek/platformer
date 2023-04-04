using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HpBar : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Material matBlink;
    private Material matDefault;

    public int hpBar;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = sprite.material;
    }
    public void TakeDamage(int Damage)
    {
        hpBar = hpBar - Damage;
        sprite.material = matBlink;
        Invoke("ResetMaterial", .2f);

    }

    void ResetMaterial()
    {
        sprite.material = matDefault;
    }
}
