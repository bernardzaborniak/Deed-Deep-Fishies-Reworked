﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * one of the basic components which are used by an entity if needed
 * counts the health 
 */

[System.Serializable]
public class HC_Health : MonoBehaviour
{
    public bool alive = true;
    public int maxHealth;
    public int currentHealth;

    public UnityEvent OnDie; //fnction assigned here gets called on die
    public UnityEvent OnTakeDamage; //fnction assigned here gets called on die

    Transform myTransform;

    bool getBackToNormalColor = false;
    float timeToGetBackToNormalColor;
    Color normalColor;
    Color damagColor;

    [Tooltip("for now used to change color on all the lods")]
    public SpriteRenderer[] renders;

    void Start()
    {
        currentHealth = maxHealth;
        normalColor = renders[0].GetComponent<SpriteRenderer>().color;
        damagColor = new Color(1 - normalColor.r, 1 - normalColor.g, 1 - normalColor.b);
    }

    void Update()
    {
        if (getBackToNormalColor)
        {
            if (Time.time >= timeToGetBackToNormalColor)
            {
                getBackToNormalColor = false;
                GetBackToNormalColor();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("Tekn damage: " + damage);
        if (alive)
        {
            currentHealth -= damage;
            if (currentHealth <= 0) Die();
        }

        OnTakeDamage.Invoke();

        //temporyli we change some colors
        for (int i = 0; i < renders.Length; i++)
        {
            renders[i].material.color = damagColor;
        }

        getBackToNormalColor = true;
        timeToGetBackToNormalColor = Time.time + 0.3f;
    }

    void Die()
    {
        alive = false;
        OnDie.Invoke();
        //entity.markAsDestroyed = true;
    }

    void GetBackToNormalColor()
    {
        for (int i = 0; i < renders.Length; i++)
        {
            renders[i].material.color = normalColor;
        }
    }
}
