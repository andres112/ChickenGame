using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour{

    public float speed = 5f;
    private Rigidbody2D rigidbody;


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
}
