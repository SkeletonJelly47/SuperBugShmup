using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    [SerializeField] bool shoot;
    [SerializeField] bool curve = true;
    [SerializeField] float curveAmt;

    public bool Shoot
    {
        get
        {
            return shoot;
        }
    }

    public bool Curve { get { return curve; }  }

    public float CurveAmt
    {
        //Setter to restrict curveAmt? Does it work in editor?? Restrict at awake?
        get
        {
            return curveAmt;
        }
    }

    private void Awake()
    {
        if (CurveAmt == 0)
        {
            curve = false;
        }
    }
}
