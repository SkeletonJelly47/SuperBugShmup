using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy
{
    Transform Weapon;
    [SerializeField] bool shootAtPlayer;
    [SerializeField] float shootingDirection;
    public Transform weaponTarget;

    protected override void Start()
    {
        base.Start();
        shootingDirection += 180;
        Weapon = transform.GetChild(0);
    }

    protected override void Shoot()
    {
        if (shootAtPlayer)
        {
            Instantiate(bulletPrefab, transform.position, Weapon.rotation);
        }
        else
        {
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
}
