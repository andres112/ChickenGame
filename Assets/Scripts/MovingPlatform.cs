using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 direction;
    private Vector3 fTurningPoint;
    private Vector3 bTurningPoint;
    public float speed;
    public bool forward;

    // Start is called before the first frame update
    void Start()
    {
        fTurningPoint = new Vector3 (transform.position.x + direction.x,transform.position.y + direction.y,transform.position.z);
        bTurningPoint = transform.position;
        direction.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >=  fTurningPoint.x && transform.position.y >=  fTurningPoint.y){
            forward = false;
        }
        if (transform.position.x <= bTurningPoint.x && transform.position.y <=  bTurningPoint.y){
            forward = true;
        }

        if(forward){
            Vector3 newPosition = new Vector3(transform.position.x + speed*direction.x,transform.position.y + speed*direction.y,transform.position.z);
            transform.position = newPosition;
        }
        else{
            Vector3 newPosition = new Vector3(transform.position.x - speed*direction.x,transform.position.y - speed*direction.y,transform.position.z);
            transform.position = newPosition;        
        }
        
    }
}
