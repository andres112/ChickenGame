using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Cache Audiomanager
    private AudioManager audioManager;
    public PlayerController thePlayer;
    private Vector3 playerCurrentPoint;

    public Cook theCook;
    private Vector3 cookCurrentPoint;

    private bool once_flag = false;

    public GameObject gameOverScreen;

    private Hashtable sounds = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        // Take the initial position 
        cookCurrentPoint = theCook.transform.position;
        playerCurrentPoint = thePlayer.transform.position;

        // Set the game over screen in false 
        gameOverScreen.SetActive(false);

        // Initialize Sounds used
        sounds.Add("background", "Background");

        //caching AudioManager
        audioManager = AudioManager.audio;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager Error: No AudioManager found in the scene.");
        }        
        
    }

    private void Update()
    {
        cookCurrentPoint = theCook.transform.position;
        playerCurrentPoint = thePlayer.transform.position;
        playerCurrentPoint.x = cookCurrentPoint.x + 10f;
        if(!once_flag){
            this.managePlaySound((string)sounds["background"]); 
            once_flag = true;
        }
    }
    public void restartGame()
    {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo()
    {
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        thePlayer.transform.position = playerCurrentPoint;
        theCook.transform.position = cookCurrentPoint;
        thePlayer.gameObject.SetActive(true);

        Health.health--;
        if (Health.health <= 0)
        {
            gameOverScreen.SetActive(true);
            Debug.Log("You are death");
            Time.timeScale = 0f;
            this.manageStopSound((string)sounds["background"]); // stop background sound when player dies
        }

    }

    // Centralized audio play trhough the Game manager
    public void managePlaySound(string _name){
        audioManager.PlaySound(_name);
    }

    public void manageStopSound(string _name){
        audioManager.StopSound(_name);
    }

}
