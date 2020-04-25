using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : MonoBehaviour {
    public Transform firePoint, shootEffect;
    public GameObject fireBall;
    // public Rigidbody2D cook;
    public int numberOfBalls = 10;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 9, 10, 11, 12 };

    private Vector2 speed;

    public float fireBallForce = 20f;
    public GameManager theGameManager;

    void Update () {

        if (GameObject.Find ("Chicken") != null) {
            float cookXpos = GameObject.Find ("Cook").transform.position.x;
            float chickenXpos = GameObject.Find ("Chicken").transform.position.x;
            if (cookXpos > chickenXpos + 1f) {
                StartCoroutine (FireLoop ());
            }
        }

        if (Fireball.IsFried) {
            theGameManager.restartGame ();
            Fireball.IsFried = false;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (list.Contains (collision.gameObject.layer)) {
            Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
        }
    }

    public IEnumerator FireLoop () {
        for (int i = 0; i < numberOfBalls; i++) {
            Fire ();
            shootEffect.GetComponent<ParticleSystem> ().enableEmission = true;
            Instantiate (shootEffect, GameObject.Find ("FirePoint").transform.position, Quaternion.identity);
            StartCoroutine (stopShootEffect ());
            yield return new WaitForSeconds (.2f);
        }
    }

    public IEnumerator stopShootEffect () {
        yield return new WaitForSeconds (0.1f);
        shootEffect.GetComponent<ParticleSystem> ().enableEmission = false;
        shootEffect.GetComponent<ParticleSystem> ().Clear ();
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