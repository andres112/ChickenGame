using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    private Transform target;
    public float moveSpeed = 3f;
    private float tempspeed = 1f;

    private Rigidbody2D rb;
    //private Vector2 movement;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 12 };
    //public Transform sectionstart;
    //public Transform sectionend;
    public Transform sightStart;
    public Transform sightEnd;
    public LayerMask ColliderPatroling;
    public LayerMask SectionEndCollider;

    private bool colliderHit_forPatrol;
    private bool follow = true;
    private bool patrol = true;
    public float chaseRadius;
    
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //rb.isKinematic = true;
    }


    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawLine(sightStart.position, sightEnd.position);
    }

    void Update()
    {

        bool colliderHit_forSectionEnd = Physics2D.Linecast(sightStart.position, sightEnd.position, SectionEndCollider);
       //Debug.Log ("**** Chicken follow" + follow);

        if (colliderHit_forSectionEnd)
        {
            follow = false;
        }

        if (!follow)
        {
            
            transform.localScale = new Vector2 (Mathf.Abs(transform.localScale.x )* -1, transform.localScale.y);
            //moveSpeed = -moveSpeed;
            //rb.velocity = new Vector2((-1) * moveSpeed, rb.velocity.y);
            transform.position =
                Vector2.MoveTowards(transform.position, target.position, moveSpeed * (-1) * Time.deltaTime);
        }
        else
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
            {
                patrol = false;
                transform.position =
                    Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            }
            else
            {
                //patrol = true;
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                colliderHit_forPatrol = Physics2D.Linecast(sightStart.position, sightEnd.position, ColliderPatroling);

                if (colliderHit_forPatrol)
                {

                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    moveSpeed *= -1;

                }
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
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.deltaTime);
    }
}