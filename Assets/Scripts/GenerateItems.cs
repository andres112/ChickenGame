using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItems : MonoBehaviour {

    private Vector3 viewPosition;
    private Vector2 screenCamLimits;
    public float seedTime = 30;
    public float timeLeft;
    private float other_timeLeft;

    System.Random ran = new System.Random ();

    [SerializeField] private List<Transform> items;
    [SerializeField] private List<Transform> bad_items;

    private void Start () {
        viewPosition = transform.position;
        timeLeft = seedTime;
        other_timeLeft = seedTime * 0.5f;
    }

    // Update is called once per frame
    void Update () {
        // update the last position of the item generator
        viewPosition = transform.position;
        viewPosition.z = 0;
        timeLeft -= Time.deltaTime; // Decrease the time to respawn some item
        if (timeLeft <= 0 && (Health.health < Health.MaxHealth | Health.shield < Health.MaxShield)) {
            int i = 0; // element in pos 0 is the shield
            if (Health.health < Health.MaxHealth) i = 1; // element in pos 1 is the a new life
            Transform currentItem = items[i];
            SpawnLevelPart (currentItem, viewPosition);
            timeLeft = Random.Range (seedTime, seedTime + 15);
        } else {
            other_timeLeft -= Time.deltaTime; // Decrease the time to respawn Bad items item
            if (other_timeLeft <= 0) {
                Transform currentItem = bad_items[ran.Next (0, bad_items.Count)];
                SpawnLevelPart (currentItem, viewPosition);
                other_timeLeft = Random.Range (seedTime * 0.1f, seedTime * 0.75f);
            }
        }
    }

    private void SpawnLevelPart (Transform item, Vector3 spawnPosition) {
        Transform itemCreated = Instantiate (item, spawnPosition, Quaternion.identity);
        // The item generated would be removed after 30 secods to mantein performance
        StartCoroutine (DestroyItem (itemCreated));
    }
    IEnumerator DestroyItem (Transform item) {
        yield return new WaitForSeconds (30f);
        if (item) { Destroy (item.gameObject); }
    }
}