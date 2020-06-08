using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    private Transform target;
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 12 };
    //public Transform sectionstart;
    //public Transform sectionend;
    public Transform sightStart;
    public Transform sightEnd;
    public LayerMask ColliderPatroling;
    public LayerMask SectionEndCollider;

    float chasingTimer = 2f;

    
    private bool colliderHit_forPatrol;
    private bool colliderHit_forSectionEnd;
    private bool follow = false;
    private bool patrol = true;
    public float chaseRadius;
    
    // Start is called before the first frame update
    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawLine(sightStart.position, sightEnd.position);
    }

    void Update()
    {
        
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            patrol = false;
            colliderHit_forSectionEnd = Physics2D.Linecast(sightStart.position, sightEnd.position, SectionEndCollider);

            if(!colliderHit_forSectionEnd)
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            
            
            if (colliderHit_forSectionEnd)
            {
                transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
  //              moveSpeed *= -1;
               //transform.Translate(Vector2.left * -3f * Time.deltaTime);
               //rb.AddForce(-transform.forward);
               transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed*(-1) * Time.deltaTime);

               //rb.velocity = new Vector3 (moveSpeed*(-1), rb.velocity.y, 0f);
               
            }

        }
        else
        {
            patrol = true;
            
        }

        if (patrol)
        {
            
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            colliderHit_forPatrol = Physics2D.Linecast(sightStart.position, sightEnd.position, ColliderPatroling);

            if (colliderHit_forPatrol) {
					
                transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
                moveSpeed *= -1;

            }
        }

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