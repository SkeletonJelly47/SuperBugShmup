using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Enemy
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
            Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 0, transform.eulerAngles.z);

            Instantiate(bulletPrefab, transform.position, Weapon.rotation);

            rot = new Vector3(transform.eulerAngles.x, Weapon.rotation.eulerAngles.y + 15f, transform.eulerAngles.z);

            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

            rot = new Vector3(transform.eulerAngles.x, Weapon.rotation.eulerAngles.y - 15f, transform.eulerAngles.z);

            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

        }
        else
        {
            Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 0 + shootingDirection, transform.eulerAngles.z);

            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

            rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 15f + shootingDirection, transform.eulerAngles.z);

            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

            rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 15f + shootingDirection, transform.eulerAngles.z);

            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));
        }

        //Instantiate(bulletPrefab, transform.position, transform.localEulerAngles(dir);

        //transform.Rotate(1, 3, 5);
    }
    // Update is called once per frame
    protected override void Update()
    {
        Weapon.transform.LookAt(weaponTarget);
        base.Update();
    }
}
