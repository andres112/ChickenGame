using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL = 15f;

    [SerializeField] private Transform level_0;
    [SerializeField] private List<Transform> levels;
    private GameObject player;
    public float help_position;

    private Vector3 lastEndPosition;

    //to check ground and to have a jumpforce we can change in the editor
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private void Awake()
    {
        player = GameObject.Find("Chicken");
        lastEndPosition = level_0.Find("End_Level").position;

        SpawnLevelPart();
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL)
        {
            SpawnLevelPart();
        }
    }

    private void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

    private void SpawnLevelPart()
    {
        Transform currentLevel = levels[Random.Range(0, levels.Count)];
        lastEndPosition.y = 0f; // ensures that the platforms are generated inside the main camera

        // Helps for player when falling down
        if(grounded){
            lastEndPosition.y = help_position;
            currentLevel = level_0;
        }
        Debug.Log("Is ground? "+grounded+" - Last end position: "+lastEndPosition.y );

        Transform lastLevelPartTransform = SpawnLevelPart(currentLevel, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("End_Level").position;
    }

    private Transform SpawnLevelPart(Transform level, Vector3 spawnPosition)
    {
        Transform levelPartTransform =  Instantiate(level, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
