using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{

    private void Awake () {
        Screen.fullScreen = true;
    }
   public void Back () {
        SceneManager.LoadScene ("MainMenu");
    }
}
