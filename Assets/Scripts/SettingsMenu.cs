using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public TMP_Dropdown graphicsDropdown;
    public TextMeshProUGUI highScore;
    public Slider volumeSlider;

    private ScoreScript score;

    private void Awake () {
        Screen.fullScreen = true;
    }

    private void Start () {
        // initialize options
        // Graphics
        if (PlayerPrefs.HasKey ("Graphics")) {
            graphicsDropdown.value = PlayerPrefs.GetInt ("Graphics");
        } else {
            graphicsDropdown.value = 2; // High graphics by default
        }
        graphicsDropdown.RefreshShownValue ();

        // Volume
        if (PlayerPrefs.HasKey ("Volume")) {
            volumeSlider.value = PlayerPrefs.GetFloat ("Volume");
        } else {
            volumeSlider.value = 0.6f; // 60% global volume by default
        }

        // HighScore
        score = new ScoreScript ();
        if (PlayerPrefs.HasKey ("HighScore")) {
            highScore.text = PlayerPrefs.GetInt ("HighScore").ToString ();
        } else {
            highScore.text = "0"; // 0 if high score does not exist before
        }
    }
    public void SetVolume (float volume) {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat ("Volume", volume);
    }

    public void SetQuality (int qualityIndex) {
        QualitySettings.SetQualityLevel (qualityIndex, true);
        PlayerPrefs.SetInt ("Graphics", qualityIndex);
    }

    public void ResetHS () {
        score.ClearHighScore ();
        highScore.text = "0";
    }
    public void Back () {
        SceneManager.LoadScene ("MainMenu");
    }
}