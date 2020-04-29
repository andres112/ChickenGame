using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {
    public TextMeshProUGUI finalScore;
    private GameManager gameManager;

    private void Awake () {
        Screen.fullScreen = true;
    }

    private void Start () {
        gameManager = new GameManager ();
        finalScore.text = ScoreScript.currentHighScore.ToString ();
    }

    // Start is called before the first frame update
    public void PlayGame () {
        SceneManager.LoadScene ("SampleScene");

        // Reset the game variables
        gameManager.ResetGameVariables ();
    }

    public void Quit () {
        SceneManager.LoadScene ("MainMenu");
        // Reset the game variables
        gameManager.ResetGameVariables ();
    }
}