using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private GameManager gameManager;

    private void Awake () {
        Screen.fullScreen = true;
    }
    private void Start () {
        gameManager = new GameManager ();
    }

    // Start is called before the first frame update
    public void PlayGame () {
        SceneManager.LoadScene ("SampleScene");

        // Reset the game variables
        gameManager.ResetGameVariables ();
    }

    public void Options () {
        PlayerPrefs.SetString ("Pre_Scene", SceneManager.GetActiveScene ().name);
        SceneManager.LoadScene ("OptionMenu");
    }

    public void Exit () {
        Debug.Log ("Good Bye!");
        Application.Quit ();

        // Reset the game variables
    }
}