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
        transform.position = fetchedWaypoints[0].transform.position;
        //Instantiate(new GameObject("OriginPoint"), fetchedWaypoints[currentWaypointIndex].transform.position + GetCurveOrigin(fetchedWaypoints[currentWaypointIndex].CurveAmt), this.transform.rotation);
        //Vector3 AO = GetCurveOrigin(fetchedWaypoints[currentWaypointIndex].CurveAmt);
        //Debug.Log(AO +)
        /*for (int i = -15; i < 15; i+=5)
        {
            Debug.Log("Point at angle: " +i + " Position: " + AO - AO.ro)
        }*/
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GetCurveOrigin(fetchedWaypoints[currentWaypointIndex].CurveAmt);
        if (!WPFinished)
        {
            if (WPReached == false)
            {
                if (distanceToWaypoint == 0)
                {
                    CalculateWaypoint();
                }

                //MoveToWaypoint();

                if (movedAmount > distanceToWaypoint) // WP Reached
                {
                    waitingAtWPArrive = true;
                    WaypointReached();
                }
            }
            /*else if(wait1 > 0 && waitingAtWPArrive == true)
            {
                wait1 -= Time.deltaTime;
            }
            else if (wait2 > 0 && waitingAtWPLeave == true)
            {
                wait2 -= Time.deltaTime;
            }*/
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

    Vector3 GetPerpendicularVector(Vector3 vec)
    {
        Debug.Log("Perpendicular vector: " + Vector3.Cross(vec, Vector3.up));
        return Vector3.Cross(vec, Vector3.up); //k1 * k2 = -1
    }
    Vector3 AO = new Vector3();
    Vector3 a, b, D;
    GameObject KP;
    float temp = 0;
    Vector3 GetCurveOrigin(float curveAmt)
    {
        //Need to restrict curves at last wp
        a = fetchedWaypoints[currentWaypointIndex].transform.position;
        b = fetchedWaypoints[currentWaypointIndex+1].transform.position;
        float distanceToTravel;

        //Debug.Log("Vec A:" + a + " Vec B: " + b);
        D = (b - a);

        //Debug.Log(GetPerpendicularVector(d));
        float x = curveAmt * (D.magnitude / 2);


        Debug.Log("x: " + x);
        Debug.Log("X hässäkkä" + ((Mathf.Pow(D.magnitude, 2f) / (8 * x)) - (x / 2)));
        //MAKE SURE THIS DOES NOT DIVIDE BY ZERO! pls
        AO = (D / 2) + GetPerpendicularVector(D).normalized * ((Mathf.Pow(D.magnitude, 2f) / (8 * x)) - (x / 2));
        Vector3 KaarenPiste;
        //Debug.Log("Kaaren piste: " + (a + AO + (GetPerpendicularVector(D).normalized * -1) * AO.magnitude));
        if (curveAmt>0)
        {
            KaarenPiste = a + AO + (GetPerpendicularVector(D).normalized * -1) * AO.magnitude;
        }
        else
        {
            KaarenPiste = a + AO + (GetPerpendicularVector(D).normalized) * AO.magnitude;
        }
        //KP = Instantiate(new GameObject("Kaaren piste"), (a + AO + (GetPerpendicularVector(D).normalized) * AO.magnitude), this.transform.rotation); 
        
        KP = Instantiate(new GameObject("Kaaren piste"), KaarenPiste, this.transform.rotation);

        temp += moveSpeed * Time.deltaTime;
        distanceToTravel = Mathf.Deg2Rad * Vector3.Angle(-AO, b - (a + AO)) * AO.magnitude;
        //transform.position = a+ AO + Vector3.RotateTowards(-AO, (b - (a + AO)), temp, 0.0f);
                                                                    //HERE TOO, U DUMMY
        transform.position = a + AO + Vector3.Slerp(-AO, (b - (a + AO)), temp/distanceToTravel);

        return AO;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AO, AO.magnitude);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(a, b);
        //Gizmos.DrawRay(a+AO, (GetPerpendicularVector(D).normalized * -1) * AO.magnitude);
        Gizmos.color = Color.magenta;
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