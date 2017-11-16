using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleWeapon : EnemyWeapon
{

    public Vector3 direction;
    bool rotate;
    // Use this for initialization
    void Shoot()
    {
        if (rotate == true)
        {
            transform.Rotate(direction);
            rotate = false;
        }
        Instantiate(bulletPrefab, transform.position, transform.rotation);

        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 15f, transform.eulerAngles.z);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 15f, transform.eulerAngles.z);

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(rot));
    }
}
