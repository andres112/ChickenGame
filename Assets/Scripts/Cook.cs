﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour {

    public float speed = 5f;
    private Rigidbody2D rigid;
    private List<int> list = new List<int> () { 0, 1, 2, 4, 5, 10, 11, 12 };

    // Start is called before the first frame update
    void Start () {
        rigid = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {
        Vector2 velocity = new Vector2 (speed, 0);
        rigid.velocity = velocity;
        // Debug.Log("**** Cook Speed:" + rigid.velocity );
    }

    private void OnCollisionEnter2D (Collision2D other) {
        // Avoid collisions with platforms
        if (list.Contains (other.gameObject.layer)) {
            Physics2D.IgnoreCollision (other.gameObject.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
        }
    }
}