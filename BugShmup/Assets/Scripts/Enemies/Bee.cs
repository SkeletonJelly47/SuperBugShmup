using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    Vector3 direction;

    protected override void Start()
    {   
        //Check if enemy rotation isn't 180 degrees
        if(transform.rotation.eulerAngles.y != 180)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        base.Start();
    }
    protected override void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
