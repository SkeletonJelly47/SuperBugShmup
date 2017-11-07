using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Enemy
{
    Vector3 direction;
    Vector3 targetLocation;

    protected override void Start()
    {
        //Check if enemy rotation isn't 180 degrees
        if (transform.rotation.eulerAngles.y != 180)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        base.Start();
    }
    protected override void Shoot()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
