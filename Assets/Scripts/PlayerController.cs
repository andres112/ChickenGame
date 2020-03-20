using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This create an object for RigidBody2D
    private Rigidbody2D rigid;
    //This will be our maximum speed as we will always be multiplying by 1
    public float maxSpeed = 2f;
    //Boolean value to represent whether we are facing left or not
    bool facingLeft = false;
    //Value to represent our Animator
    Animator anim;

    //to check ground and to have a jumpforce we can change in the editor
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        //set anim to our animator
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set our vertical Speed
        anim.SetFloat("vSpeed", rigid.velocity.y);
        //set our grounded bool
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //set ground in our Animator to match grounded
        anim.SetBool("Ground", grounded);
        // get horizontal movement
        float move = Input.GetAxis("Horizontal");//Gives us of one if we are moving via the arrow keys
                                                 //move our Players rigidbody

        // The movement is only restrited to right
        if (move >= 0)
        {
            rigid.velocity = new Vector2((1 + move) * maxSpeed, rigid.velocity.y);            
        }
        else
        {
            rigid.velocity = new Vector2((0.3f + Mathf.Abs(move)), rigid.velocity.y);            
        }

        //set our speed
        anim.SetFloat("Speed", Mathf.Abs((1)));

        ////if we are moving left but not facing left flip, and vice versa
        //if (move < 0 && !facingLeft)
        //{
        //    Flip();
        //}
        //else if (move > 0 && facingLeft)
        //{
        //    Flip();
        //}
    }

    void Update()
    {
        //if we are on the ground and the space bar was pressed, change our ground state and add an upward force
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigid.AddForce(new Vector2(0, jumpForce));
        }
    }

    // detect collision and destroy element (corn)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Items")
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject); // destroy the item wich collides
        }

    }

    //flip if needed
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
