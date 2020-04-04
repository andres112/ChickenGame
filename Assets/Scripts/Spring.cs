using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Vector2 impulse;
    public Animator springAnimationController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void OnTriggerEnter2D(Collider2D collider){
             if (collider.gameObject.tag == "Player")
             {
                 Rigidbody2D collider_rigidbody = collider.GetComponent<Rigidbody2D>();
                 Vector2 velocity = new Vector2(collider_rigidbody.velocity.x,0);
                 collider_rigidbody.velocity = velocity;

                 collider_rigidbody.AddForce (impulse, ForceMode2D.Impulse);
                 if(springAnimationController != null){
                     Debug.Log ("Animation!");
                    springAnimationController.SetTrigger("active");
                 }
             }
         }
}
