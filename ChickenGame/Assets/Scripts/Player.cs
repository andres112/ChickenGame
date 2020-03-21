using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform jumpRaycastPosition;
    public float jumpRaycastRadius;
    public LayerMask jumpLayerMask;
    public float speed = 5f;
    public float constRspeed = 5f;
    public float jumpValue = 8f;
    private Rigidbody2D rigidbody;
    public Animator playerAnimationController;
    public GameManager theGameManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //walk
        float horizontalValue = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(horizontalValue*speed + constRspeed,rigidbody.velocity.y);
        rigidbody.velocity = velocity;

        //animation walk signal
        
        if(playerAnimationController != null){
            playerAnimationController.SetFloat("speedx", Mathf.Abs(horizontalValue*speed + constRspeed));
        }
        
        //jump
        bool jumpIsDown = Input.GetButtonDown("Jump");
        bool isGrounded = Physics2D.OverlapCircle(jumpRaycastPosition.position, jumpRaycastRadius,jumpLayerMask);
        if(jumpIsDown && isGrounded){
            //animation jump signal
            
            if(playerAnimationController != null){
                playerAnimationController.SetTrigger("jump");
            }
            
            rigidbody.AddForce(new Vector2(0, jumpValue), ForceMode2D.Impulse);
        }
        else{
            if(playerAnimationController != null){
                playerAnimationController.ResetTrigger("jump");
            }
            
        }

        if(isGrounded){
            //animation grounded signal
            
            if(playerAnimationController != null){
                playerAnimationController.SetTrigger("grounded");
            }
        }
        else{
            if(playerAnimationController != null){
                playerAnimationController.ResetTrigger("grounded");
            }
        }

        //mirror sprite
        Vector3 scale = transform.localScale;
        if(velocity.x < 0){
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (velocity.x > 0){
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;



    }

    public void OnTriggerEnter2D(Collider2D collider){
        if ( collider.tag == "Deadly Hazard"){
            theGameManager.restrartGame();
        }
    }

}
