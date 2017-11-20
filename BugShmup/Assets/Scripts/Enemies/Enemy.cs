using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Enemy properties
    [SerializeField]
    private int health;
    protected bool alive;
    [SerializeField]
    protected float moveSpeed;
    public GameObject bulletPrefab;
    public GameObject pickupPrefab;
    BoxCollider collider;

    //WP container
    public GameObject WaypointContainer;

    //Linear waypoint variables
    List<EnemyWaypoint> Waypoints;
    int WPIndex = 0;
    bool WPReached = false;
    bool WPFinished = false;
    float distanceToWaypoint = 0;
    float movedAmount = 0f;
    Vector3 directionToWaypoint;

    //Curve waypoint variables
    Vector3 AO = new Vector3();
    Vector3 a, b, D, curvePoint;
    float x = 0;
    //Rename pls
    float temp = 0;
    float curveCompletetion;

    //Wait variables
    public float wait1, wait2;
    bool waitingAtWPLeave = false;
    bool waitingAtWPArrive = false;
    bool hasShot = false;

    //Pickup variables
    bool drop;


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
                if (drop)
                {
                    Instantiate(pickupPrefab, transform.position, transform.rotation);
                }
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
        Waypoints = WaypointContainer.GetComponentsInChildren<EnemyWaypoint>().ToList();
        if (pickupPrefab == null)
        {
            drop = false;
        }
        wait1 = Waypoints[0].WaitArrive;
        wait2 = Waypoints[0].WaitLeave;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!WPFinished)
        {
            if (WPReached == false)
            {
                if (distanceToWaypoint == 0 && Waypoints[WPIndex].Curve == false)
                {
                    CalculateWaypoint();
                    GetWaitTimes();
                }
                else if (distanceToWaypoint == 0 && Waypoints[WPIndex].Curve)
                {
                    CalculateCurve(Waypoints[WPIndex].CurveAmt);
                    GetWaitTimes();
                }

                //No curve
                if (Waypoints[WPIndex].Curve == false)
                {
                    MoveToWaypoint();
                }
                //Curve
                else if (Waypoints[WPIndex].Curve)
                {
                    MoveToWaypoint(Waypoints[WPIndex].CurveAmt);
                }
                //WP Reached
                if (movedAmount > distanceToWaypoint || curveCompletetion >= 1)
                {
                    waitingAtWPArrive = true;
                    WPReached = true;
                    //WaypointReached();
                }
            }
            if (WPReached == true)
            {
                //Waiting at arriving
                if (wait1 > 0 && waitingAtWPArrive == true)
                {
                    wait1 -= Time.deltaTime;
                }
                else
                {
                    waitingAtWPArrive = false;
                    waitingAtWPLeave = true;
                    //Shoot or no shoot
                    if (Waypoints[WPIndex].Shoot)
                    {
                        if (hasShot == false)
                        {
                            Shoot();
                            hasShot = true;
                        }
                    }
                }

                //Waiting before leaving
                if (wait2 > 0 && waitingAtWPLeave == true && waitingAtWPArrive == false)
                {
                    wait2 -= Time.deltaTime;
                }
                else if(wait2 <= 0)
                {
                    waitingAtWPLeave = false;
                    WaypointReached();
                }
            }
        }
        else
        {
            // K  Y  S  !  !
            DestroySelf();
        }

    }

    /// <summary>
    /// Populates the linear waypoint variables
    /// </summary>
    void CalculateWaypoint()
    {
        //ok
        distanceToWaypoint = (Waypoints[WPIndex].transform.position - transform.position).magnitude;
        directionToWaypoint = (Waypoints[WPIndex].transform.position - transform.position).normalized;
    }

    void GetWaitTimes()
    {
        wait1 = Waypoints[WPIndex].WaitArrive;
        wait2 = Waypoints[WPIndex].WaitLeave;
    }

    /// <summary>
    /// Populates the curve waypoint variables
    /// </summary>
    /// <param name="curveAmt">Negative values makes curve go to left side, positive values go right. 1 is a half circle and 0 is invalid.</param>
    void CalculateCurve(float curveAmt)
    {
        //Check if index is over bounds eg. a is the last checkpoint
        if (WPIndex < Waypoints.Count)
        {
            //Get a and b at current waypoints
            a = transform.position;
            b = Waypoints[WPIndex].transform.position;
            //Make vector D
            D = b - a;
            //Calculate x variable
            x = curveAmt * (D.magnitude / 2);
            //Calculate AO
            AO = (D / 2) + GetPerpendicularVector(D).normalized * ((Mathf.Pow(D.magnitude, 2f) / (8 * x)) - (x / 2));
            distanceToWaypoint = Mathf.Deg2Rad * Vector3.Angle(-AO, b - (a + AO)) * AO.magnitude;
            //Apply rotation by adding to transform the final position vector eg. a + AO + slerp thingy
            //->MoveToWaypoint
        }
        else
            Debug.Log("Index would go out of range, end of waypoints");
    }

    void WaypointReached()
    {
        //Go to next waypoint index       
        WPIndex++;

        //Reset loop variables
        movedAmount = 0f;
        distanceToWaypoint = 0f;
        directionToWaypoint = Vector3.zero;
        WPReached = false;

        //Reset curve variables
        a = Vector3.zero;
        b = Vector3.zero;
        D = Vector3.zero;
        AO = Vector3.zero;
        curveCompletetion = 0f;
        temp = 0f;
        x = 0f;

        //Reset wait variables
        wait1 = 0;
        wait2 = 0;
        waitingAtWPLeave = false;
        waitingAtWPArrive = false;
        hasShot = false;

        //Shoot or no shooterino
        /*if (Waypoints[WPIndex - 1].Shoot)
        {
            Shoot();
        }*/

        if (WPIndex > Waypoints.Count - 1)
        {
            WPFinished = true;
        }
    }

    /// <summary>
    /// Moves the enemy linearly towards next waypoint
    /// </summary>
    void MoveToWaypoint()
    {
        transform.position += directionToWaypoint * Time.deltaTime * moveSpeed;
        movedAmount += (directionToWaypoint * Time.deltaTime * moveSpeed).magnitude;
    }
    /// <summary>
    /// Moves the enemy in a curve towards next waypoint
    /// </summary>
    /// <param name="curveAmt">Negative values makes curve go to left side, positive values go right. 1 is a half circle and 0 is invalid.</param>
    void MoveToWaypoint(float curveAmt)
    {
        temp += moveSpeed * Time.deltaTime;
        curveCompletetion = temp / distanceToWaypoint;
        transform.position = a + AO + Vector3.Slerp(-AO, (b - (a + AO)), curveCompletetion);
    }

    Vector3 GetPerpendicularVector(Vector3 vec)
    {
        return Vector3.Cross(vec, Vector3.up);
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