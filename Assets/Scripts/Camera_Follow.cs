using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;
    public float camera_distance = 30.0f;

    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / camera_distance);
    }

    void FixedUpdate()
    {
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = new Vector3(player.position.x, (player.position.y*10)/100, transform.position.z);
    }
}
