using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
    private AudioManager audioManager;
    public Transform hitEffect;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 11, 12 };

    public static bool IsFried = false;

    private void Start () {
        //caching AudioManager
        audioManager = AudioManager.audio;
        if (audioManager == null) {
            Debug.LogError ("AudioManager Error: No AudioManager found in the scene.");
        }  
        else{
            audioManager.PlaySound ("Cannon");
        }      
        StartCoroutine ("DestroyBall");
    }
    private void OnCollisionEnter2D (Collision2D collision) {
        if (list.Contains (collision.gameObject.layer)) {
            Physics2D.IgnoreCollision (collision.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
        }

        if (collision.gameObject.tag == "Player") {
            hitEffect.GetComponent<ParticleSystem> ().enableEmission = true;
            Instantiate (hitEffect, transform.position, Quaternion.identity);
            audioManager.PlaySound ("Die");
            StartCoroutine (stopEffect ());
            IsFried = true;
            Destroy (gameObject);
        }

        if (collision.gameObject.layer == 10) {
            hitEffect.GetComponent<ParticleSystem> ().enableEmission = true;
            Instantiate (hitEffect, transform.position, Quaternion.identity);
            StartCoroutine (stopEffect ());
            Destroy (gameObject);
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator stopEffect () {
        yield return new WaitForSeconds (0.3f);
        hitEffect.GetComponent<ParticleSystem> ().enableEmission = false;
        hitEffect.GetComponent<ParticleSystem> ().Clear ();
    }    

    IEnumerator DestroyBall () {
        yield return new WaitForSeconds (1f);
        Destroy (gameObject);
    }
}