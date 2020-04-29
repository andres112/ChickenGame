using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start () {
        gameManager = new GameManager ();
        pauseMenuUI.SetActive (false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape) & !GameManager.IsGameOver) {
            if (GameIsPaused) {
                Resume ();
            } else {
                Pause ();
            }
        }
    }

    public void Resume () {
        pauseMenuUI.SetActive (false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause () {
        pauseMenuUI.SetActive (true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart () {
        GameIsPaused = false;
        gameManager.ResetGameVariables ();
        SceneManager.LoadScene ("SampleScene");      
    }
    public void Quit () {
        GameIsPaused = false;
        SceneManager.LoadScene ("MainMenu");
        // Reset the game variables
        gameManager.ResetGameVariables ();
    }
}