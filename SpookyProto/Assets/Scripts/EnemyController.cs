using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //Roaming
    public Transform[] points;
    private int destPoint = 0;
    private bool isRoaming = false;

    //Attacking
    public Transform playerTf;
    public NavMeshAgent agent;
    public float maxAngle;
    public float maxRadius;
    public float heightMultiplayer = 1.5f;
    public bool isInFov;
    private float stopFollowing;
    public float DisengageDistance;
    private bool Stun = false;
    // Start is called before the first frame update
    void Start()
    {
        //Allows for smooth movement between points
        agent.autoBraking = false;
        GoToNextPoint();
    }

    // Update is called once per frame
 

    //Makes the stun effect
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Light"))
        {
            Debug.Log("Ouch");
            Stun = true;
            //Starts timer for stun
            StartCoroutine(TimerForStun());
        }
       
    }

     //Roaming plan
     void GoToNextPoint()
    {

        if (isInFov == false && isRoaming == true)
        {
            //Nothing happens if no points are set
            if (points.Length == 0)
            {
                return;
            }
            //goes to point
            agent.destination = points[destPoint].position;
            //sets to next point and loops it to start
            destPoint = (destPoint + 1) % points.Length;
        }
    }

    IEnumerator TimerForStun()
    {
        yield return new WaitForSeconds(3);
        Stun = false;
    }

    //Visualizes detection
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (isInFov == false)
        {
            Gizmos.color = Color.red;
        }
        if (isInFov == true)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawRay(transform.position, (playerTf.transform.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    //Controls the logic of following and detecting player
    //checking object is the enemy's location, target is player, max angle is how wide they can "see", and radius is how far they can see
    public void inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        //Gets the direction of the enemy to the target
        Vector3 directionBetween = (target.position - checkingObject.position).normalized;
        //they should be face level
        directionBetween.y *= 0;

        //Send out raycast and defines space of it
        RaycastHit hit;
        if (Physics.Raycast(checkingObject.position + Vector3.zero, (target.position - checkingObject.position).normalized, out hit, maxRadius))
        {
            //if the raycast hits the player
            if (LayerMask.LayerToName(hit.transform.gameObject.layer) == "Player")
            {
                // Gets the angle of the player
                float angle = Vector3.Angle(checkingObject.forward + Vector3.zero, directionBetween);
                
                //if in the correct angle; start chase
                if (angle <= maxAngle)
                {
                    isInFov = true;
                }
                else
                {
                    isInFov = false;
                }
                
                
            }
            else
            {
                isInFov = false;
            }
        }

    }
void FixedUpdate()
    {
        if(isInFov == false)
        {
            //if a path is ready to be used
            if (!agent.pathPending && agent.remainingDistance <= 0.5f)
            {
                GoToNextPoint();
            }
        }


        //Finds distance between player and enemy
        stopFollowing = Vector3.Distance(playerTf.position, transform.position);

        //Makes the enemy stop chasing after getting too far
        if (stopFollowing >= DisengageDistance)
        {
            isInFov = false;
            isRoaming = true;
        }
        //Updates cords tracking player based on the information we give it(the transform, playertf and so on)
        inFOV(transform, playerTf, maxAngle, maxRadius);
       
        //if enemy not stunend and he is in fov, follow
        if (Stun == false && isInFov == true)
          {
              // Follows player
              agent.destination = playerTf.position;

          } else if( Stun == true)
          { 
              //Stop right where the enemy is
              agent.destination = transform.position;

          } else if (isInFov == false)
        {
            isRoaming = true;
        }
     

    }
}



