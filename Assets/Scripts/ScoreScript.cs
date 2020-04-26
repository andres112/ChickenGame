using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    public static ScoreScript instance;
    public static int scoreValue, highScore, currentHighScore;
    public TextMeshProUGUI scoreUI, highScoreUI;
    // Start is called before the first frame update

    private void Awake () {
        instance = this;
        // Validate if HighScore variable exist in memory
        if (PlayerPrefs.HasKey ("HighScore")) {
            highScore = PlayerPrefs.GetInt ("HighScore");
        }
    }

    // Update is called once per frame
    void Update () {
        // Update UI
        scoreUI.text = "X " + scoreValue;
        highScoreUI.text = highScore.ToString ();
        UpdateHighScore ();
        UpdateCurrentHighScore ();
    }

    // Normal score section
    public void AddScore () {
        scoreValue++;
    }
    public void ReduceScore () {
        scoreValue--;
    }
    public void ReduceScore (int q) {
        scoreValue = (scoreValue < q || scoreValue <= 0) ? 0 : scoreValue - q;
    }
    public void ResetScore () {
        scoreValue = 0;
    }

    // High score section
    public void UpdateHighScore () {
        highScore = scoreValue > highScore ? scoreValue : highScore;
        // Save in local memory space
        PlayerPrefs.SetInt ("HighScore", highScore);
    }
    // Delete the global high score (only in game options)
    public void ClearHighScore(){
        PlayerPrefs.DeleteKey("HighScore");
    }

    // Current High score section
    public void UpdateCurrentHighScore () {
        currentHighScore = scoreValue > currentHighScore ? scoreValue : currentHighScore;
    }
    public void ResetCurrentHighScore () {
        currentHighScore = 0;
    }

}