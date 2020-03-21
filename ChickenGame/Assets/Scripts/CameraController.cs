using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float distanceToMove;
    public Cook theCook;
    private Vector3 lastCookPosition;

    // Start is called before the first frame update
    void Start()
    {
        theCook = FindObjectOfType<Cook>();
        lastCookPosition = theCook.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToMove = theCook.transform.position.x - lastCookPosition.x;
        transform.position =  new Vector3(transform.position.x + distanceToMove ,transform.position.y,transform.position.z);

        lastCookPosition = theCook.transform.position;
        
    }
}
