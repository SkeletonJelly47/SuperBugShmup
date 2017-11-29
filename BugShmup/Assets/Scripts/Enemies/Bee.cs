using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    Transform Weapon;
    [SerializeField] bool shootAtPlayer;
    [SerializeField] float shootingDirection;
    public Transform weaponTarget;

    GameObject target;
    Vector3 targetLocation;
    protected override void Start()
    {
        base.Start();
        Weapon = transform.GetChild(0);
        shootingDirection += 180;
        target = GameObject.FindWithTag("Player");
        weaponTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void Shoot()
    {
        if (shootAtPlayer)
        {
            Instantiate(bulletPrefab, transform.position, Weapon.rotation);
        }
        else
        {
            /* Use this if you need enemy to rotate towards player while shooting
            RotateObject(); */
            Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + shootingDirection, transform.eulerAngles.z);
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));
        }


    }

    // Update is called once per frame
    protected override void Update()
    {
        Weapon.transform.LookAt(weaponTarget);
        base.Update();
    }
    private void RotateObject()
    {
        targetLocation = target.GetComponent<Transform>().position;
        transform.LookAt(targetLocation);
    }
}
