using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionGenerator : MonoBehaviour
{
    public GameObject[] theSections;
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
            int index = Random.Range(0,theSections.Length);
            platformWidth = theSections[index].GetComponent<BoxCollider2D>().size.x * theSections[index].transform.localScale.x;
            transform.position = new Vector3(transform.position.x +platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Instantiate(theSections[index], transform.position, transform.rotation);
        }
    }
}
