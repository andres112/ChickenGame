using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : MonoBehaviour {
    public Transform firePoint, shootEffect;
    public GameObject fireBall;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 9, 10, 11, 12 };

    private Vector2 speed;

    public float fireBallForce = 20f;
    public float  fireRate=0.2f;
    public GameManager theGameManager;
    static bool nextball = true;

    void Update () {

        if (GameObject.Find ("Chicken") != null) {
            float cookXpos = GameObject.Find ("Cook").transform.position.x;
            float chickenXpos = GameObject.Find ("Chicken").transform.position.x;
            if (cookXpos > chickenXpos && nextball) {
                nextball = false;
                StartCoroutine (FireLoop ());
            }
        }

        if (Fireball.IsFried) {
            theGameManager.restartGame ();
            Fireball.IsFried = false;
            // Destroy all fireball instances after chicken is dead
            GameObject[] fireball_instances = GameObject.FindGameObjectsWithTag("Fireball"); 
            foreach(GameObject fireball_instance in fireball_instances)
            {
                Destroy(fireball_instance);
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (list.Contains (collision.gameObject.layer)) {
            Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
        }
    }

    public IEnumerator FireLoop () {
        Fire ();
        shootEffect.GetComponent<ParticleSystem> ().enableEmission = true;
        Instantiate (shootEffect, GameObject.Find ("FirePoint").transform.position, Quaternion.identity);
        StartCoroutine (stopShootEffect ());
        yield return new WaitForSeconds (fireRate);
        nextball = true;
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