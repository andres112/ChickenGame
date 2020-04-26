using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI finalScore;

    private void Start() {
        finalScore.text = ScoreScript.currentHighScore.ToString();
    }

    // Start is called before the first frame update
    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");

        // Reset the game variables
        GameManager.IsGameOver = false;
        Health.health = 5;
        ScoreScript.instance.ResetScore();
        ScoreScript.instance.ResetCurrentHighScore();
    }

    public void Quit(){
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
