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
    private bool follow = false;
    public float chaseRadius;

    [SerializeField] private float _rayCastOffset = 5f;
    [SerializeField] private float _rayCastDistance = 10f;
    private float _moveDir = -1;

    // Start is called before the first frame update
    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void TurnLeft()
    {
        //sets the movement direction to -1 to make the gameObject move left
        _moveDir = 1;
    }
 
    void TurnRight()
    {
        //sets the movement direction to 1 to make the gameObject move right
        _moveDir = -1;
    }
 
    void Move()
    {
        //Setting up the start position of both raycasts
        Vector2 rayCastOriginRight = transform.position + new Vector3(_rayCastOffset, 0, 0);
        //Only difference is that we flip the rayCastOffset because we want it to point towards the left, therefore the "-" in front
        Vector2 rayCastOriginLeft = transform.position + new Vector3(-_rayCastOffset, 0, 0);
 
        if (Physics2D.Raycast(rayCastOriginRight, Vector2.right,_rayCastDistance))
        {
            TurnLeft();
        }
        if (Physics2D.Raycast(rayCastOriginLeft, Vector2.left,_rayCastDistance))
        {
            TurnRight();
        }
 
        //Moves the Gameobject every frame based on the _moveDir variable;
        transform.Translate(new Vector2(_moveDir * moveSpeed * Time.deltaTime, 0));
 
 
        //Debug rays to visualize the raycasts, can be deleted, has no impact on gameplay;
        Debug.DrawRay(rayCastOriginRight, Vector2.right * _rayCastDistance, Color.red);
        Debug.DrawRay(rayCastOriginLeft, Vector2.left * _rayCastDistance, Color.blue);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            follow = true;
        }
        else
        {
            follow = false;
        }

        if (!follow)
        {
            Move();

            //transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
      /*      RaycastHit2D sectionstarthit = Physics2D.Raycast(sectionstart.position, Vector2.zero);
            RaycastHit2D sectionendhit = Physics2D.Raycast(sectionend.position, Vector2.left);
            if (sectionendhit.collider != null)
            {
                if (colliderhit)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    colliderhit = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    colliderhit = true;
                }

            }*/
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