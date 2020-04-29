using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Controller : MonoBehaviour
{
    public Animator checkpointAnimationController;
    private bool activated;
// Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void setActive()
    {
        activated = true;
        if (checkpointAnimationController != null)
        {
            checkpointAnimationController.SetTrigger("active");
        }
    }

    public void OnTriggerEnter2D(Collider2D collider){
             if (collider.gameObject.tag == "Player" && !activated)
             {
            activated = true;
                 //increase level
                 LevelScript.levelValue++;
                 if(checkpointAnimationController != null){
                    checkpointAnimationController.SetTrigger("active");
                 }
             }
         }
}
