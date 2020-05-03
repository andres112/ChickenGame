using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour {
    public bool destroyObject;
    public Transform bottom;

    public void OnTriggerEnter2D (Collider2D collision) {
        // Destroy other element
        if (collision.gameObject.layer == 12 && destroyObject) {
            Destroy(collision.gameObject);
        }
        // Destroy element itself
        if (collision.gameObject.tag == "Deadly Hazard" && destroyObject) {
            Destroy(gameObject);
        }
    }
}