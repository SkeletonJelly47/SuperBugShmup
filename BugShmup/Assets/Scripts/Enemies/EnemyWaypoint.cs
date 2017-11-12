using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    [SerializeField] bool shoot;
    [SerializeField] bool curve;
    [SerializeField] float curveAmt;

    public bool Shoot
    {
        get
        {
            return shoot;
        }
    }

    public float CurveAmt
    {
        //Setter to restrict curveAmt? Does it work in editor?? Restrict at awake?
        get
        {
            return curveAmt;
        }
    }
}
