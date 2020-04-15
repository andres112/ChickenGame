using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {
    private int dayLength; //in minutes
    private int dayStart;
    private int nightStart; //also in minutes
    private int currentTime;
    public float cycleSpeed;
    private bool isDay;
    private Vector3 sunPosition;
    public Light sun, moon;
    public GameObject earth, stars;

    public float days_to_win = 1;
    float count_days = 0;

    void Start () {
        dayLength = 1440;
        dayStart = 330;
        nightStart = 1140;
        currentTime = 300;
        StartCoroutine (TimeOfDay ());
        earth = gameObject.transform.parent.gameObject;
        moon.intensity = 15;
        moon.range = 10;
    }

    void Update () {
        float currentTimeF = currentTime;
        float dayLengthF = dayLength;

        if (currentTime > 0 && currentTime < dayStart) {
            isDay = false;
            sun.intensity = 0;
            stars.SetActive (true);
        } else if (currentTime >= dayStart && currentTime < nightStart) {
            isDay = true;
            sun.intensity = 1;
            sun.range = 25;
            sun.colorTemperature = 450f;
            stars.SetActive (false);
        } else if (currentTime >= nightStart && currentTime < dayLength) {
            isDay = false;
            sun.intensity = 0;
            stars.SetActive (true);
        } else if (currentTime >= dayLength) {
            currentTime = 0;
        }

        // every N cycles the player win a hearth if health is less than MaxHealth
        if (currentTime == 300) {
            count_days++;
            if (count_days >= days_to_win) {                
                if (Health.health < Health.MaxHealth) {
                    Health.health++;
                }
                Debug.LogWarning ("New Level Generation Activated. new health: "+Health.health);
                count_days = 0;
            }
        }

        earth.transform.eulerAngles = new Vector3 (0, 0, (-(currentTimeF / dayLengthF) * 360) + 90);
    }

    IEnumerator TimeOfDay () {
        while (true) {
            currentTime += 1;
            int hours = Mathf.RoundToInt (currentTime / 60);
            int minutes = currentTime % 60;
            Debug.Log (hours + ":" + minutes);
            yield return new WaitForSeconds (1F / cycleSpeed);
        }
    }
}