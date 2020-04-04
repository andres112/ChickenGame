using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItems : MonoBehaviour
{

    private Vector3 viewPosition;
    private Vector2 screenCamLimits;
    public float seedTime = 30;
    public float timeLeft;

    [SerializeField] private List<Transform> items;

    private void Start()
    {
        viewPosition = transform.position;
        timeLeft = seedTime;
    }

    // Update is called once per frame
    void Update()
    {
        // update the last position of the item generator
        viewPosition = transform.position;
        viewPosition.z = 0;

        timeLeft -= Time.deltaTime; // Decrease the time to respawn some item
        if (timeLeft <= 0 && (Health.health < Health.MaxHealth | Health.shield < Health.MaxShield))
        {
            int i = 0; // element in pos 0 is the shield
            if (Health.health < Health.MaxHealth) i = 1; // element in pos 1 is the a new life
            Transform currentItem = items[i];
            SpawnLevelPart(currentItem, viewPosition);

            timeLeft = Random.Range(seedTime, seedTime+15);
        }
    }

    private void SpawnLevelPart(Transform item, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(item, spawnPosition, Quaternion.identity);
    }
}
