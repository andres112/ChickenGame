using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour{

    public float speed = 5f;
    private float speedStore;
    public float speedMulitplier = 1.05f;
    public float speedMilestone = 50;
    private float speedMilestoneCount;
    private float speedMilestoneCountStore;
    private Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody =  GetComponent<Rigidbody2D>();
        speedMilestoneCount = speedMilestone;

        speedStore = speed;
        speedMilestoneCountStore = speedMilestone;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(speed,rigidbody.velocity.y);
        rigidbody.velocity = velocity;

        if (transform.position.x > speedMilestoneCount){
            speed = speed * speedMulitplier;
            speedMilestone += speedMilestone * speedMulitplier;
            speedMilestoneCount += speedMilestone;
        }

    }

    public void resetPrivateVariables()
    {
        StartCoroutine ("ResetPrivateVariablesCo");
    }

    public void ResetPrivateVariablesCo()
    {
        speed = speedStore;
        speedMilestoneCount = speedMilestoneCountStore;

    }
}
