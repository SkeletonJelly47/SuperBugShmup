using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    [SerializeField] bool shoot;
    [SerializeField] bool curve = true;
    [Range(-1f ,1f)]
    [SerializeField] float curveAmt;
    [SerializeField] float waitArrive;
    [SerializeField] float waitLeave;

    public bool Shoot
    {
        get
        {
            return shoot;
        }
    }

    public bool Curve { get { return curve; }  }

    public float CurveAmt { get { return curveAmt; } }

    public float WaitArrive { get { return waitArrive; } set { waitArrive = value; } }

    public float WaitLeave { get { return waitLeave; } set { waitLeave = value; } }

    private void Awake()
    {
        //Limit curve values
        if (CurveAmt == 0)
        {
            curve = false;
        }
        else if (curveAmt < -1)
        {
            curveAmt = -1;
        }
        else if (curveAmt > 1)
        {
            curveAmt = 1;
        }
        //Limit wait values
        else if (waitArrive < 0)
        {
            waitArrive = 0;
        }
        else if (waitLeave < 0)
        {
            waitLeave = 0;
        }
    }
}
