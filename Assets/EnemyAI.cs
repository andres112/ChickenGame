using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
   
    public float speed = 200f;

    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;
    
    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        var bounds = GetComponent<Collider2D>().bounds;
// Expand the bounds along the Z axis
        bounds.Expand(Vector3.forward*1000);
        var guo = new GraphUpdateObject(bounds);
// change some settings on the object
      
        AstarPath.active.UpdateGraphs(guo);
     */  
        if(enemyGFX is null)      
            enemyGFX = GameObject.FindGameObjectWithTag("BirdGFX_Tag").GetComponent<Transform>();  

        
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

  /*      // Check if the first grid graph in the scene has any nodes
        // if it doesn't then it is not scanned.
        if (AstarPath.active.data.gridGraph.nodes == null) AstarPath.active.Scan();
        AstarPath.FindAstarPath();
        AstarPath.active.Scan();
   */     
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("UpdatePath", 0f, .5f);

    }
    
    void UpdatePath()
    {
        
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);

    }
    
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        } else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

       
        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f,1f, 1f);
        }else if (rb.velocity.x <= -0.01f)
        {
            enemyGFX.localScale =  new Vector3(1f, 1f, 1f);
        }
        
    } 
}
 