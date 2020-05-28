using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    private Transform target;
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 11, 12 };
    private Transform flag;


    // Start is called before the first frame update
    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       // flag = GameObject.FindGameObjectWithTag("checkpoint_section").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
      //  if(flag.position.x >= target.position.x)
      //  {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    /*    }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, flag.position, moveSpeed * Time.deltaTime);

        }
        */
        /*
             Vector3 direction = target.position - transform.position;
             float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
             rb.rotation = angle;
             direction.Normalize();
             movement = direction;
         */   
    }
    private void FixedUpdate() {
        
        if (transform.position.x < target.position.x)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        
        //moveCharacter(movement);
    }
    
    
    private void OnCollisionEnter2D (Collision2D other) {
        // Avoid collisions with platforms
        if (list.Contains (other.gameObject.layer)) {
            Physics2D.IgnoreCollision (other.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
        }
    }
    
    void moveCharacter(Vector2 direction){
        // rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
    }
}