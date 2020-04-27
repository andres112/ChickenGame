using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private GameManager gameManager;

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
        Debug.Log ("Options Button");
    }

    public void Exit () {
        Debug.Log ("Good Bye!");
        Application.Quit ();

        // Reset the game variables
    }
}