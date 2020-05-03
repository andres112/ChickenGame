using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItems : MonoBehaviour {

    private Vector3 viewPosition;
    private Vector2 screenCamLimits;
    public float seedTime = 30;
    public float timeLeft;
    private float bad_items_timeLeft, power_timeLeft;

    System.Random ran = new System.Random ();

    [SerializeField] private List<Transform> items;
    [SerializeField] private List<Transform> bad_items;

    private void Start () {
        viewPosition = transform.position;
        timeLeft = seedTime;
        bad_items_timeLeft = seedTime * 0.5f;
        power_timeLeft = seedTime * 0.9f;
    }

    // Update is called once per frame
    void Update () {
        // update the last position of the item generator
        viewPosition = transform.position;
        viewPosition.z = 0;
        timeLeft -= Time.deltaTime; // Decrease the time to respawn some item
        power_timeLeft -= Time.deltaTime; // Decrease the time to respawn Power item
        if (timeLeft <= 0 && (Health.health < Health.MaxHealth | Health.shield < Health.MaxShield)) {
            int i = 0; // element in pos 0 is the shield
            if (Health.health < Health.MaxHealth) i = 1; // element in pos 1 is the a new life
            Transform currentItem = items[i];
            SpawnLevelPart (currentItem, viewPosition);
            timeLeft = Random.Range (seedTime, seedTime + 15);
        } else if (power_timeLeft <= 0 && Power.power < Power.MaxPower) {
            Transform currentItem = items[2]; // Thunder item
            SpawnLevelPart (currentItem, viewPosition);
            power_timeLeft = Random.Range (seedTime * 1.1f, seedTime * 1.5f);
        } else {
            bad_items_timeLeft -= Time.deltaTime; // Decrease the time to respawn Bad items item
            if (bad_items_timeLeft <= 0) {
                Transform currentItem = bad_items[ran.Next (0, bad_items.Count)];
                SpawnLevelPart (currentItem, viewPosition);
                bad_items_timeLeft = Random.Range (seedTime * 0.1f, seedTime * 0.5f);
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