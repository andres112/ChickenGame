using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLine : MonoBehaviour {
    private Rigidbody2D rigidbody;
    public Transform fire;
    public Rigidbody2D cook;
    public GameManager theGameManager;

    private Vector2 speed;
    // Start is called before the first frame update
    void Start () {
        fire.GetComponent<ParticleSystem> ().enableEmission = false;
        rigidbody =  GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        Vector2 pos = new Vector2(cook.position.x - 2f, 0);
        rigidbody.position = pos;               
    }

    private void OnCollisionEnter2D (Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Fried");
            fire.GetComponent<ParticleSystem> ().enableEmission = true;
            theGameManager.managePlaySound ("Die");
            StartCoroutine(stopFire());
        }        
    }

    public IEnumerator stopFire () {        
        yield return new WaitForSeconds (0.5f);
        theGameManager.restartGame ();
        fire.GetComponent<ParticleSystem> ().enableEmission = false;
        fire.GetComponent<ParticleSystem> ().Clear();
    }
}