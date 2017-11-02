using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    protected bool alive;
    BoxCollider collider;
    [SerializeField] protected float moveSpeed;

    //Change to WP container!
    public GameObject WaypointContainer;

    //Waypoint variables
    List<EnemyWaypoint> fetchedWaypoints;
    int currentWaypointIndex = 0;
    bool WPReached = false;
    bool WPFinished = false;
    float movedAmount = 0f;
    float distanceToWaypoint = 0;
    Vector3 directionToWaypoint;



    protected int Health
    {
        get
        {
            return health;
        }

        set
        {
            //dead
            health = value;
            if (health < 0)
            {
                alive = false;
                DestroySelf();
            }
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        collider = GetComponent<BoxCollider>();

        //I knew there was an easier way!
        fetchedWaypoints = WaypointContainer.GetComponentsInChildren<EnemyWaypoint>().ToList();

        //Not sure if there's an easier way for this but it'll do
        /*for (int i = 0; i < Waypoints.Count ; i++)
        {
            //Check for empty list!
            fetchedWaypoints[i] = Waypoints[i].GetComponent<EnemyWaypoint>();
        }*/
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!WPFinished)
        {
            if (WPReached == false)
            {
                if (distanceToWaypoint == 0)
                {
                    distanceToWaypoint = (fetchedWaypoints[currentWaypointIndex].transform.position - transform.position).magnitude;
                    directionToWaypoint = (fetchedWaypoints[currentWaypointIndex].transform.position - transform.position).normalized;
                }
                MoveToWaypoint();
                if (movedAmount > distanceToWaypoint) // WP Reached
                {
                    WPReached = true;
                }

            }
            else
            {
                transform.position = fetchedWaypoints[currentWaypointIndex].transform.position;
                currentWaypointIndex++;
                movedAmount = 0f;
                distanceToWaypoint = 0f;
                WPReached = false; //Hetkinen
                if (currentWaypointIndex > fetchedWaypoints.Count-1)
                {
                    WPFinished = true;
                }
                //index++, WPReached = true, set pos to exactly to WP pos, movedAmount to zero, 
            } 
        }
        else
        {
            // K  Y  S  !  !
            DestroySelf();
        }

    }

    void MoveToWaypoint()
    {
        transform.position += directionToWaypoint * Time.deltaTime * moveSpeed;
        movedAmount += (directionToWaypoint * Time.deltaTime * moveSpeed).magnitude;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}