﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpingWolf : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 fTurningPoint;
    private Vector3 bTurningPoint;
    private Vector3 uTurningPoint;
    public Vector2 delta;
    public Vector2 speed;
    public bool forward;
    public bool upward;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fTurningPoint = new Vector3(transform.position.x + delta.x , transform.position.y, transform.position.z);
        uTurningPoint = new Vector3(transform.position.x, transform.position.y + delta.y/2, transform.position.z);
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
            if (transform.position.y >= uTurningPoint.y)
            {
                upward = false;
            }
            else if (transform.position.y <= fTurningPoint.y)
            {
                upward = true;
            }

            if (forward)
            {
                if (upward) {
                    Vector3 newPosition = new Vector3(transform.position.x + speed.x, transform.position.y + speed.y, transform.position.z);
                    transform.position = newPosition;
                }
                else
                {
                    Vector3 newPosition = new Vector3(transform.position.x + speed.x, transform.position.y - speed.y, transform.position.z);
                    transform.position = newPosition;
                }
            }
            else
            {
                if (upward)
                {
                    Vector3 newPosition = new Vector3(transform.position.x - speed.x, transform.position.y + speed.y, transform.position.z);
                    transform.position = newPosition;
                }
                else
                {
                    Vector3 newPosition = new Vector3(transform.position.x - speed.x, transform.position.y - speed.y, transform.position.z);
                    transform.position = newPosition;
                }
            }
        }
    }
}