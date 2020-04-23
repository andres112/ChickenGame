using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Controller : MonoBehaviour
{
    public Animator checkpointAnimationController;
    private bool active;
// Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void OnTriggerEnter2D(Collider2D collider){
             if (collider.gameObject.tag == "Player" && !active)
             {
                active = true;
                 //increase level
                 LevelScript.levelValue++;
                 if(checkpointAnimationController != null){
                    checkpointAnimationController.SetTrigger("active");
                 }
             }
         }
}
