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
    public GameObject bulletPrefab;

    //Change to WP container!
    public GameObject WaypointContainer;

    //Waypoint variables
    List<EnemyWaypoint> fetchedWaypoints;
    int currentWaypointIndex = 0;
    bool WPReached = false;
    bool WPFinished = false;
    bool waitingAtWPLeave = false;
    bool waitingAtWPArrive = false;

    public float wait1, wait2;

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
                    CalculateWaypoint();
                }

                MoveToWaypoint();

                if (movedAmount > distanceToWaypoint) // WP Reached
                {
                    waitingAtWPArrive = true;
                    WaypointReached();
                }
            }
            else if(wait1 > 0 && waitingAtWPArrive == true)
            {
                wait1 -= Time.deltaTime;
            }
            else if (wait2 > 0 && waitingAtWPLeave == true)
            {
                wait2 -= Time.deltaTime;
            }
        }
        else
        {
            // K  Y  S  !  !
            DestroySelf();
        }

    }

    void CalculateWaypoint()
    {
        distanceToWaypoint = (fetchedWaypoints[currentWaypointIndex].transform.position - transform.position).magnitude;
        directionToWaypoint = (fetchedWaypoints[currentWaypointIndex].transform.position - transform.position).normalized;
    }

    void WaypointReached()
    {
        //Set position to wp to prevent going over the waypoint
        transform.position = fetchedWaypoints[currentWaypointIndex].transform.position;

        //Go to next waypoint index
        //currentWaypointIndex = Mathf.Clamp(currentWaypointIndex + 1, 0, fetchedWaypoints.Count - 1);
        currentWaypointIndex++;

        //Reset loop variables
        movedAmount = 0f;
        distanceToWaypoint = 0f;
        WPReached = false;

        //Shoot or no shooterino
        if (fetchedWaypoints[currentWaypointIndex-1].Shoot)
        {
            Shoot();
        }

        if (currentWaypointIndex > fetchedWaypoints.Count-1)
        {
            WPFinished = true;
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
    protected abstract void Shoot();


    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}