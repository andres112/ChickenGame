using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform pfDoubleJumpEffect;

    //This create an object for RigidBody2D
    private Rigidbody2D rigid;

    //This will be our maximum speed as we will always be multiplying by 1
    public float maxSpeed = 2f;

    //Boolean value to represent whether we are facing left or not
    bool facingLeft = false;

    //Value to represent our Animator
    Animator anim;

    // for jumping, the counts
    private int airJumpCount;
    public float jumpvelocity = 4f;
    float jumpTimeCounter = 0.60f;
    public float jumpTime;
    private bool isfirstJumping;

    //to check ground and to have a jumpforce we can change in the editor
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;

    // Velocity settings
    public float InertiaSpeed, AccelerationSpeed, GroundSpeed, SkySpeed;

    // Control variables when restrictions are enabled
    private float Original_AccelerationSpeed, Original_SkySpeed, Original_GroundSpeed;

    // Validate is on platforms
    private bool IsPlatform = false;
    private string timerOnBy;

    // The game manager
    public GameManager theGameManager;

    // Sounds for the player item - name 
    // 0 - CornSound
    // 1 - JumpSound
    // 2 - DeathSound
    // 3 - ShieldSound
    // 4 - NewLifeSound
    // 5 - IceCubeSound
    // 6 - AnvilSound
    // 7 - ThunderSound
    // 8 - SpeedUpSound
    // 9 - NoJumpSound
    public string[] soundNames;

    public TextMeshProUGUI timeLeftText;
    private bool canJump, IsIceCube;

    private void Awake()
    {
        airJumpCount = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        //set anim to our animator
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        Original_AccelerationSpeed = AccelerationSpeed;
        Original_SkySpeed = SkySpeed;
        Original_GroundSpeed = GroundSpeed;
        canJump = true;
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
        float move = Input.GetAxis("Horizontal"); //Gives us of one if we are moving via the arrow keys
        //move our Players rigidbody

        // The movement is only restrited to right
        if (move >= 0)
        {
            if (grounded)
            {
                // validate if ground in platform or floor
                float variationSpeed = IsPlatform ? AccelerationSpeed : GroundSpeed;
                rigid.velocity = new Vector2((variationSpeed + move) * maxSpeed, rigid.velocity.y);
            }
            else
            {
                // Jumping x velocity
                rigid.velocity = new Vector2((SkySpeed + move) * maxSpeed, rigid.velocity.y);
            }
        }
        else
        {
            rigid.velocity = new Vector2((InertiaSpeed + Mathf.Abs(move)), rigid.velocity.y);
        }

        // Debug.Log ("**** Chicken Speed:" + rigid.velocity);

        //set our speed
        anim.SetFloat("Speed", Mathf.Abs((1)));
    }

    void Update()
    {
        // Verify the status of player acording to restrictions
        this.CheckRestrictions(timerOnBy);
        
        if (grounded)
        {
            rigid.gravityScale = 1;
            isfirstJumping = true;
            jumpTimeCounter = jumpTime;
            airJumpCount = 2;
        }

        //if we are on the ground and the space bar was pressed, change our ground state and add an upward force
        // 

        if (Input.GetKey(KeyCode.Space))
        {
            if (!canJump)
            {
                theGameManager.managePlaySound("NoJump");
            }
            else if (isfirstJumping)
            {

                if (grounded)
                {
                    theGameManager.managePlaySound(soundNames[1]);
                }

                if (jumpTimeCounter > 0) {
                    // Normal Jump
                   anim.SetBool("Ground", false);
                    rigid.velocity = Vector2.up * jumpvelocity;
                    rigid.gravityScale = 0.9f;

                    jumpTimeCounter -= Time.deltaTime;

                    // Sound When player jump
                }else 
                {
                    isfirstJumping = false;
                }
       
            }
            else  if (Input.GetKeyDown(KeyCode.Space) & ScoreScript.scoreValue > 0)
            {
                    if (airJumpCount > 0)
                    {
                        // Air Jump
                        anim.SetBool("Ground", false);
                        if (airJumpCount == 2)
                        {
                            rigid.gravityScale = 0.8f;
                            rigid.velocity = Vector2.up * (jumpvelocity + 1f);
                            ScoreScript.instance.ReduceScore(1);
                            theGameManager.managePlaySound(soundNames[1]);
                        }
                        else if (airJumpCount == 1)
                        {
                            Instantiate(pfDoubleJumpEffect, transform.position, Quaternion.identity);
                            rigid.gravityScale = 0.7f;
                            rigid.velocity = Vector2.up * (jumpvelocity);
                            ScoreScript.instance.ReduceScore(2);
                            theGameManager.managePlaySound(soundNames[1]);

                        }
                        airJumpCount--;
                    }
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            rigid.gravityScale = 1;
            isfirstJumping = false;
        }
      
        // When player click on Up arrow to increase velocity temporarly
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Power.power > 0 && !CountDown.IsTimerOn)
        {
            CountDown.timeLeft = 2f;
            CountDown.IsTimerOn = true;
            timerOnBy = "Thunder";
            anim.speed = 1.7f;
            Power.DecreasePower();
            theGameManager.managePlaySound(soundNames[8]);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // rapid drop 
            rigid.velocity = Vector2.down * jumpvelocity;
        }
    }

    // detect collision and trigger destroy element (corn)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // item collider
        if (collision.gameObject.layer == 12)
        {
            if (collision.gameObject.tag == "Corn")
            {
                ScoreScript.instance.AddScore(); // increase the score every time collide with a corn
                theGameManager.managePlaySound(soundNames[0]);
            }

            if (!(collision.gameObject.tag == "Stationary"))
            {
                collision.gameObject.SetActive(false);
                Destroy(collision.gameObject); // destroy the item wich collides
            }
        }
        else
        {
            if (collision.gameObject.tag == "Goal")
            {
                theGameManager.winGame();
            }
        }
    }

    // Detect collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Platform Collision
        IsPlatform = false;
        if (collision.gameObject.layer == 10)
        {
            // by default the platforms give to the player more speed
            IsPlatform = true;
        }

        // Enemy collider
        if (collision.gameObject.layer == 11)
        {
            if (collision.gameObject.tag == "Deadly Hazard")
            {
                theGameManager.restartGame();
                theGameManager.managePlaySound(soundNames[2]);
            }

            // Wolf collider and restart score
            if (collision.gameObject.tag == "Damaging Hazard")
            {
                theGameManager.respawnGame();
                theGameManager.managePlaySound(soundNames[2]);
            }
        }

        if (collision.gameObject.layer == 12)
        {
            // Shield collision
            if (collision.gameObject.tag == "Shield" & Health.shield < Health.health)
            {
                // increase the shield every time collide with a blue hearth if the shield is less than health
                Health.IncreaseShield();
                theGameManager.managePlaySound(soundNames[3]);
            }

            // New Life Collision
            if (collision.gameObject.tag == "Life")
            {
                Health.IncreaseHealth(); // increase the shield every time collide with a blue hearth
                theGameManager.managePlaySound(soundNames[4]);
            }

            // Thunder Collision - Power up
            if (collision.gameObject.tag == "Thunder")
            {
                Power.IncreasePower(); // increase the power every time collide with a thunder
                theGameManager.managePlaySound(soundNames[7]);
            }

            // Ice Cube Collision
            if (collision.gameObject.tag == "Ice Cube")
            {
                theGameManager.managePlaySound(soundNames[5]);
                CountDown.timeLeft = 10f;
                CountDown.IsTimerOn = true;
                timerOnBy = "Ice Cube";
                timeLeftText.gameObject.SetActive(true);
                theGameManager.manageStopSound("SpeedUp"); // Stop Speed up sound
                // Modify Background pitch for slow motion sensation
                theGameManager.managePauseSound("Background");
                theGameManager.managePitchSound("Background", 0.8f);
                theGameManager.once_flag = false;
                IsIceCube = true;
            }

            // Anvil Collision
            if (collision.gameObject.tag == "Anvil")
            {
                theGameManager.managePlaySound(soundNames[6]);
                CountDown.timeLeft = 5f;
                CountDown.IsTimerOn = true;
                timerOnBy = "Anvil";
                timeLeftText.gameObject.SetActive(true);
            }

            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject); // destroy the item wich collides
        }
    }

    
    void CheckRestrictions(string restriction)
    {
        GroundSpeed = Original_GroundSpeed;
        if (CountDown.IsTimerOn)
        {
            switch (restriction)
            {
                case "Ice Cube":
                    anim.SetBool("Metal", false);
                    anim.SetBool("Super", false);
                    anim.SetBool("Frozen", true);
                    AccelerationSpeed = GroundSpeed;
                    SkySpeed = GroundSpeed;
                    anim.speed = 0.5f;
                    break;
                case "Anvil":
                    anim.SetBool("Metal", true);
                    anim.SetBool("Super", false);
                    anim.SetBool("Frozen", false);
                    canJump = false;
                    SkySpeed = GroundSpeed;
                    break;
                case "Thunder":
                    anim.SetBool("Super", true);
                    anim.SetBool("Metal", false);
                    anim.SetBool("Frozen", false);
                    GroundSpeed = 1;
                    AccelerationSpeed = GroundSpeed;
                    SkySpeed = GroundSpeed;
                    break;
            }
        }
        else
        {
            AccelerationSpeed = Original_AccelerationSpeed;
            SkySpeed = Original_SkySpeed;
            canJump = true;
            anim.SetBool("Metal", false);
            anim.SetBool("Super", false);
            anim.SetBool("Frozen", false);
            anim.speed = 1;
            theGameManager.manageStopSound("SpeedUp");
            // First is required to pause the audio to reconfigure the features//
            if (IsIceCube)
            {
                theGameManager.managePauseSound("Background");
                theGameManager.managePitchSound("Background", 0.95f);
                theGameManager.once_flag = false;
                IsIceCube = false;
            }

            //******************************************//
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