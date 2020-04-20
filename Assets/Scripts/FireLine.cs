using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : MonoBehaviour {
    public Transform firePoint;
    public GameObject fireBall;
    public Rigidbody2D cook;
    public int numberOfBalls = 10;

    private Vector2 speed;

    public float fireBallForce = 20f;
    public GameManager theGameManager;

    void Update () {
        Vector2 pos = new Vector2 (cook.position.x - 2f, cook.position.y);
        GetComponent<Rigidbody2D> ().position = pos;
        if(Fireball.IsFried){
            theGameManager.restartGame();
            Fireball.IsFried = false;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine (FireLoop ());
        }
    }

    public IEnumerator FireLoop () {
        for (int i = 0; i < numberOfBalls; i++) {
            Fire ();
            yield return new WaitForSeconds (0.1f);
        }
    }

    void Fire () {
        // Spawning the fireball
        GameObject fire = Instantiate (fireBall, firePoint.position, firePoint.rotation);
        // Access to the Rigidbody 2D of the fireball Gameobject
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D> ();
        // Add force to the fireball from the firepoint 
        rb.AddForce (firePoint.up * fireBallForce, ForceMode2D.Impulse);
    }

}