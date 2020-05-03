using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour {
    public static int power = 1;

    public const int MaxPower = 3;

    public Image[] thunders;
    public Sprite thunder; // thunder

    private void Update () {
        for (int i = 0; i < thunders.Length; i++) {
            if (i < power) {
                thunders[i].sprite = thunder;
                thunders[i].enabled = true;
            } else {
                thunders[i].enabled = false;
            }
        }
    }

    public static void IncreasePower () {
        if (power < MaxPower) power++;
    }

    public static void DecreasePower () {
        if (power > 0) power--;
    }
}