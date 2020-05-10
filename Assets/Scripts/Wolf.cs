using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 fTurningPoint;
    private Vector3 bTurningPoint;
    public int delta;
    public float speed;
    public bool forward;
    //public float jumpForce;
    //public LayerMask groundLayers;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fTurningPoint = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
        bTurningPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsGameOver)
        {
            if (transform.position.x >= fTurningPoint.x)
            {
                forward = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.position.x <= bTurningPoint.x)
            {
                forward = true;
                //Rotate the Wolf at new Position
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (forward)
            {
                Vector3 newPosition = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                transform.position = newPosition;
            }
            else
            {
                Vector3 newPosition = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                transform.position = newPosition;
            }

           // rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

            /*if (transform.position.x > fTurningPoint.x)
            {
                Vector3 newPosition = new Vector3(transform.position.x, fTurningPoint.y, transform.position.z);
                transform.position = newPosition;
                forward = false;
            }
            if (transform.position.x < bTurningPoint.x)
            {
                Vector3 newPosition = new Vector3(transform.position.x, bTurningPoint.y, transform.position.z);
                transform.position = newPosition;
                forward = true;
            }*/
        }
       
    }
}
