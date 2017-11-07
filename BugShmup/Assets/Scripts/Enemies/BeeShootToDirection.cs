using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeShootToDirection : Enemy
{
    GameObject target;
    public Vector3 direction;
    Vector3 targetLocation;
    bool rotate;

    protected override void Start()
    {   
        //Check if enemy rotation isn't 180 degrees
        if (transform.rotation.eulerAngles.y != 180)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        target = GameObject.FindGameObjectWithTag("Player");
        rotate = true;
        base.Start();
    }
    protected override void Shoot()
    {   
        if(rotate == true)
        {
            transform.Rotate(direction);
            rotate = false;
        }
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }
}
