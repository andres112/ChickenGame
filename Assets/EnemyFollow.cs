using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    private Transform target;
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 12 };
    public Transform sectionstart;
    public Transform sectionend;
    private bool colliderhit = true;
    public float chaseRadius;



    // Start is called before the first frame update
    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        /*       RaycastHit2D sectionstarthit = Physics2D.Raycast(sectionstart.position, Vector2.zero);
               RaycastHit2D sectionendhit = Physics2D.Raycast(sectionend.position, Vector2.zero);
   
               if (sectionendhit.collider == null)
               {
   
                       transform.position =
                           Vector2.MoveTowards(transform.position, sectionstart.position, moveSpeed * Time.deltaTime);
                       Debug.Log(sectionendhit.collider.name);
      
     
               }
               else
               {
   
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