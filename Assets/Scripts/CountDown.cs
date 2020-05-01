using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI timeLeftText;
    public static float timeLeft = 5f;
    public static bool IsTimerOn = false;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        // Debug.Log("timeLeft: "+timeLeft);
        if(timeLeft < 0){
            timeLeft = 0;
            timeLeftText.gameObject.SetActive(false);
            CountDown.IsTimerOn = false;
        }

        timeLeftText.text = Mathf.Round(timeLeft).ToString();
        // PauseMenu.GameIsPaused;
        
    }
}
