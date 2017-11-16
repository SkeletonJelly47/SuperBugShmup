using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Enemy
{
    public float rotation;
    public Transform weaponTarget;
    bool rotate;
    Transform Weapon;

	protected override void Start ()
    {
        base.Start();
        Weapon = transform.GetChild(0);
    }

    protected override void Shoot()
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 0 + rotation, transform.eulerAngles.z);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 15f + rotation, transform.eulerAngles.z);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 15f + rotation, transform.eulerAngles.z);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));


        //Instantiate(bulletPrefab, transform.position, transform.localEulerAngles(dir);

        //transform.Rotate(1, 3, 5);
    }

    // Update is called once per frame
    protected override void Update ()
    {
        Weapon.transform.LookAt(weaponTarget);
        base.Update();
	}
}
