using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL = 50f;

    [SerializeField] private Transform level_0;
    [SerializeField] private List<Transform> levels;
    private GameObject player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        player = GameObject.Find("Chicken");


        lastEndPosition = level_0.Find("End_Level").position;
        for (int i=0; i<3; i++)
        {            
            SpawnLevelPart();
        }            
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform currentLevel = levels[Random.Range(0, levels.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(currentLevel, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("End_Level").position;
    }

    private Transform SpawnLevelPart(Transform level, Vector3 spawnPosition)
    {
        Transform levelPartTransform =  Instantiate(level, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
