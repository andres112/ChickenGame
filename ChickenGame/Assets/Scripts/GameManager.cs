using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGeneration;
    private Vector3 platformStrartPoint;

    public Player thePlayer;
    private Vector3 playerStratPoint;
    private float playerStartSpeed;
    

    public Cook theCook;
    private Vector3 cookStartPoint;

    private PlatformDestructor[] platformList;

    // Start is called before the first frame update
    void Start()
    {
        platformStrartPoint = platformGeneration.transform.position;

        playerStratPoint = thePlayer.transform.position;

        cookStartPoint = theCook.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restrartGame()
    {
        StartCoroutine ("RestrartGameCo");
    }

    public IEnumerator RestrartGameCo()
    {
        theCook.resetPrivateVariables();
        thePlayer.resetPrivateVariables();
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        platformList = FindObjectsOfType<PlatformDestructor>();
        for(int i = 0; i < platformList.Length; i++){
           Destroy( platformList[i].gameObject);
        }
        thePlayer.transform.position = playerStratPoint;
        platformGeneration.transform.position = platformStrartPoint;
        theCook.transform.position = cookStartPoint;
        thePlayer.gameObject.SetActive(true);

    }
}
