﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static int health = 4;
    public static int shield = 0;

    public const int MaxHealth = 5;
    public const int MaxShield = 5;

    public Image[] hearths;
    public Sprite full; // red hearth
    public Sprite armor; // blue hearth

    private void Update()
    {
        for (int i = 0; i < hearths.Length; i++)
        {
            if(i < shield){
                hearths[i].sprite = armor; 
                hearths[i].enabled = true;
                continue;
            }
            if (i < health)
            {
                hearths[i].sprite = full; 
                hearths[i].enabled = true;
            }
            else
            {
                hearths[i].enabled = false;
            }
        }
    }

    public static void IncreaseHealth () {
        if (health < MaxHealth) health++;
    }

    public static void DecreaseHealth () {
        if (health > 0) health--;
    }

    public static void IncreaseShield () {
        if (shield < MaxShield) shield++;
    }

    public static void DecreaseShield () {
        if (shield > 0) shield--;
    }
}
