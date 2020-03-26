using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] thePlatforms;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x){
            int index = Random.Range(0,thePlatforms.Length);
            platformWidth = thePlatforms[index].GetComponent<BoxCollider2D>().size.x;
            transform.position = new Vector3(transform.position.x +platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Instantiate(thePlatforms[index], transform.position, transform.rotation);
        }
    }
}
