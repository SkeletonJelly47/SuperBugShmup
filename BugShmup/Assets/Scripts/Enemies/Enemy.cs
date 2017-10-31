using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    protected bool alive;
    BoxCollider collider;
    //Change to WP container!
    public List<GameObject> Waypoints;
    //give me a proper name ffs
    List<EnemyWaypoint> fetchedWaypoints;
    int currentWaypointIndex = 0;
    bool WPReached = false;

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
        //Not sure if there's an easier way for this but it'll do
        for (int i = 0; i < Waypoints.Count ; i++)
        {
            //Check for empty list!
            fetchedWaypoints[i] = Waypoints[i].GetComponent<EnemyWaypoint>();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (WPReached == false)
        {
            //move towards wp
        }
        else
        {
            //index++, WPReached = true, set pos to exactly to WP pos
        }

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