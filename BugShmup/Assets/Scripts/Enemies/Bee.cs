using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    Vector3 direction;
    GameObject target;
    public Vector3 targetLocation;

    public Transform weaponTarget;
    Transform Weapon;

    protected override void Start()
    {   
        base.Start();
        Weapon = transform.GetChild(0);
    }
    protected override void Shoot()
    {
        targetLocation = target.GetComponent<Transform>().position;
        transform.LookAt(targetLocation);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    protected override void Update()
    {   
        base.Update();
        Weapon.transform.LookAt(weaponTarget);

    }
}
