using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");
        // Reset the game variables
        GameManager.IsGameOver = false;
        Health.health = 5;
        ScoreScript.scoreValue = 0;
    }

    public void Quit(){
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
