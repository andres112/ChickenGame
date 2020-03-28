using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerController thePlayer;
    private Vector3 playerCurrentPoint;

    public Cook theCook;
    private Vector3 cookCurrentPoint;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Take the initial position 
        cookCurrentPoint = theCook.transform.position;
        playerCurrentPoint = thePlayer.transform.position;

        // Set the game over screen in false 
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        cookCurrentPoint = theCook.transform.position;
        playerCurrentPoint = thePlayer.transform.position;
        playerCurrentPoint.x = cookCurrentPoint.x + 10f;
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
        if(Health.health <= 0){
            gameOverScreen.SetActive(true);
            Debug.Log("You are death");
            Time.timeScale = 0f;
        }

    }

}
