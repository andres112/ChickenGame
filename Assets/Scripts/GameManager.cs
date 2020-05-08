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

    public SectionGenerator platformGeneration;
    private Vector3 platformStartPosition;

    public PlayerController thePlayer;
    private Vector3 playerStartPosition;

    public float invincibilityLength;
    private float invincibilityCounter;
    public float blinkLength;
    private float blinkCounter;

    public Vector2 hurtImpulse;

    public Cook theCook;
    private Vector3 cookStartPosition;

    private ObjectDestroyer[] objectList;

    public GameObject startAreaFlag;

    private int currentLevel;

    public bool once_flag = false;

    public GameObject countdown;

    private Hashtable sounds = new Hashtable ();

    private void Awake() {
        Screen.fullScreen = true;
    }

    // Start is called before the first frame update
    void Start () {
        // Countdown before to start
        StartCoroutine ("StartDelay");
        GameObject.Find ("TimeLeft").SetActive(false);
        // Take the initial position 

        platformStartPosition = platformGeneration.transform.position;
        cookStartPosition = theCook.transform.position;
        playerStartPosition = thePlayer.transform.position; 


        startAreaFlag.SetActive(false);
        

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
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            if (blinkCounter > 0)
            {
                blinkCounter -= Time.deltaTime;
            }
            else
            {
                blinkCounter = blinkLength;
                thePlayer.GetComponent<SpriteRenderer>().enabled = !thePlayer.GetComponent<SpriteRenderer>().enabled;
            }
        }
        else
        {
            thePlayer.GetComponent<SpriteRenderer>().enabled = true;
        }

        

        if (!once_flag) {
            managePlaySound ((string) sounds["background"]);
            once_flag = true;
        }
        if (currentLevel < LevelScript.levelValue) {
            thePlayer.maxSpeed = thePlayer.maxSpeed + thePlayer.maxSpeed * speedUpFactor;
            // thePlayer.GroundSpeed = thePlayer.GroundSpeed + thePlayer.GroundSpeed*speedUpFactor;
            // thePlayer.SkySpeed = thePlayer.SkySpeed + thePlayer.SkySpeed*speedUpFactor;

            theCook.speed = theCook.speed + speedUpFactor * theCook.speed ;

            currentLevel = LevelScript.levelValue;
        }


        // Volume
        if (PlayerPrefs.HasKey ("Volume")) {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        }  
        else{
            AudioListener.volume = 0.6f; // High graphics by default
        } 
    }

    // Reset Game Variables
    public void ResetGameVariables(){
        Health.health = 5;
        Health.shield = 0;
        ScoreScript.instance.ResetScore();
        ScoreScript.instance.ResetCurrentHighScore();
        GameManager.IsGameOver = false;
    }

    IEnumerator StartDelay () {
        // Corutine for initial countdown
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

    public void winGame()
    {
        this.manageStopSound((string)sounds["background"]); // stop background sound when player wins
        SceneManager.LoadScene("WinScreen");
    }

    public IEnumerator RestartGameCo () {
        CountDown.timeLeft = 0;
        startAreaFlag.SetActive(true);
        startAreaFlag.GetComponent<Animator>().SetTrigger("active");

        thePlayer.gameObject.SetActive (false);
        yield return new WaitForSeconds (0.5f);

        objectList = FindObjectsOfType<ObjectDestroyer>();
        for (int i = 0; i < objectList.Length; i++)
        {
            Destroy(objectList[i].gameObject);
        }

        thePlayer.transform.position = playerStartPosition;
        theCook.transform.position = cookStartPosition;
        platformGeneration.transform.position = platformStartPosition;
        platformGeneration.resetPrivateVariables();

        thePlayer.gameObject.SetActive (true);

        // Lifes and shields decresing logic
        if (Health.health == Health.shield) {
            Health.DecreaseShield();
        }
        Health.DecreaseHealth();;
        checkIsAlive ();
    }
    public IEnumerator RespawnGameCo () {
        startAreaFlag.SetActive(true);
        startAreaFlag.GetComponent<Animator>().SetTrigger("active");    
        if (invincibilityCounter <= 0)
        {
            // Lifes and shields decresing logic
            if (Health.shield > 0)
            {
                Health.DecreaseShield();
                //hurt + invincibility frames
                invincibilityCounter = invincibilityLength;
                blinkCounter = blinkLength;
                thePlayer.GetComponent<SpriteRenderer>().enabled = false;
                //knockback when hurt
                Rigidbody2D player_rigidbody = thePlayer.GetComponent<Rigidbody2D>();
                Vector2 velocity = new Vector2(player_rigidbody.velocity.x, 0);
                player_rigidbody.velocity = velocity;

                player_rigidbody.AddForce(hurtImpulse, ForceMode2D.Impulse);
            }
            else
            {
                //Respawn at the beginning of the level
                Health.DecreaseHealth();

                thePlayer.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                thePlayer.transform.position = playerStartPosition;
                theCook.transform.position = cookStartPosition;
                platformGeneration.transform.position = platformStartPosition;
                thePlayer.gameObject.SetActive(true);
            }
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

    public void managePauseSound (string _name) {
        audioManager.PauseSound (_name);
    }

    public void managePitchSound (string _name, float newPitch) {
        audioManager.UpdatePitch (_name, newPitch);
    }

}