using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static bool IsGameOver = false;
    // Cache Audiomanager
    private AudioManager audioManager;

    public float speedUpFactor;
    public PlayerController thePlayer;
    private Vector3 playerCurrentPoint;

    public Cook theCook;
    private Vector3 cookCurrentPoint;

    private int currentLevel;

    private bool once_flag = false;

    public GameObject countdown;

    private Hashtable sounds = new Hashtable ();

    // Start is called before the first frame update
    void Start () {
        // Countdown before to start
        StartCoroutine ("StartDelay");
        // Take the initial position 
        cookCurrentPoint = theCook.transform.position;
        playerCurrentPoint = thePlayer.transform.position;

        // TODO: // Set the game over screen in false 
        // gameOverScreen.SetActive(false);

        // Initialize Sounds used
        sounds.Add ("background", "Background");

        //caching AudioManager
        audioManager = AudioManager.audio;
        if (audioManager == null) {
            Debug.LogError ("AudioManager Error: No AudioManager found in the scene.");
        }

        currentLevel = 0;

    }

    private void Update () {
        cookCurrentPoint = theCook.transform.position;
        playerCurrentPoint = thePlayer.transform.position;
        playerCurrentPoint.x = cookCurrentPoint.x + 10f;
        playerCurrentPoint.y = -3; // over the ground
        if (!once_flag) {
            this.managePlaySound ((string) sounds["background"]);
            once_flag = true;
        }
        if (currentLevel < LevelScript.levelValue) {
            thePlayer.maxSpeed = thePlayer.maxSpeed + thePlayer.maxSpeed * speedUpFactor;
            // thePlayer.GroundSpeed = thePlayer.GroundSpeed + thePlayer.GroundSpeed*speedUpFactor;
            // thePlayer.SkySpeed = thePlayer.SkySpeed + thePlayer.SkySpeed*speedUpFactor;

            theCook.speed = theCook.speed + speedUpFactor * theCook.speed ;

            currentLevel = LevelScript.levelValue;
        }
    }

    IEnumerator StartDelay () {

        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3.5f;
        while (Time.realtimeSinceStartup < pauseTime) {
            yield return 0;
        }

        countdown.SetActive (false);
        if (!PauseMenu.GameIsPaused) Time.timeScale = 1;
    }
    public void restartGame () {
        StartCoroutine ("RestartGameCo");
    }

    public void respawnGame () {
        StartCoroutine ("RespawnGameCo");
    }

    public IEnumerator RestartGameCo () {
        thePlayer.gameObject.SetActive (false);
        yield return new WaitForSeconds (0.5f);

        thePlayer.transform.position = playerCurrentPoint;
        theCook.transform.position = cookCurrentPoint;
        thePlayer.gameObject.SetActive (true);

        // Lifes and shields decresing logic
        if (Health.health == Health.shield) {
            Health.shield--;
        }
        Health.health--;
        checkIsAlive ();
    }
    public IEnumerator RespawnGameCo () {
        thePlayer.gameObject.SetActive (false);
        yield return new WaitForSeconds (0.2f);

        thePlayer.transform.position = playerCurrentPoint;
        theCook.transform.position = cookCurrentPoint;
        thePlayer.gameObject.SetActive (true);

        // Lifes and shields decresing logic
        if (Health.shield > 0) {
            Health.shield--;
        } else {
            Health.health--;
        }
        checkIsAlive ();
    }

    private void checkIsAlive () {
        if (Health.health <= 0) {
            IsGameOver = true;
            this.manageStopSound ((string) sounds["background"]); // stop background sound when player dies
            SceneManager.LoadScene ("GameOver");
        }
    }

    // Centralized audio play trhough the Game manager
    public void managePlaySound (string _name) {
        audioManager.PlaySound (_name);
    }

    public void manageStopSound (string _name) {
        audioManager.StopSound (_name);
    }

}