using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    Vector3 point;
    [SerializeField] bool shoot;

    public bool Shoot
    {
        get
        {
            return shoot;
        }
    }

    void Start()
    {
            
    }


}
