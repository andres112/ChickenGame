using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public float speed;
    private Transform target;
    // Start is called before the first frame update
    void Start () {
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
        Vector3 direction = target.position -  transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}