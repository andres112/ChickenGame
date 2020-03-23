using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour{

    public float speed = 5f;
    private Rigidbody2D rigidbody;
    private List<int> list = new List<int>() { 10, 12};


    // Start is called before the first frame update
    void Start()
    {
        rigidbody =  GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(speed,rigidbody.velocity.y);
        rigidbody.velocity = velocity;               
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Avoid collisions with platforms
        if(list.Contains(other.gameObject.layer)){
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }        
    }
}
