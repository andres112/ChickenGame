using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelScript : MonoBehaviour
{
    public static int levelValue = 0;
    private TextMeshProUGUI level;
    // Start is called before the first frame update
    void Start()
    {
        level = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        level.text = "Level " + levelValue;
    }
}
