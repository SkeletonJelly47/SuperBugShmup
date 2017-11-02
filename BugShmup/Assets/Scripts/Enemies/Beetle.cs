using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Enemy
{
    public Beetle()
    {

    }

	protected override void Start ()
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
        Instantiate(bulletPrefab, transform.position, transform.rotation);

        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 15f, transform.eulerAngles.z);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 15f, transform.eulerAngles.z);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));


        //Instantiate(bulletPrefab, transform.position, transform.localEulerAngles(dir);

        //transform.Rotate(1, 3, 5);
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
	}
}
