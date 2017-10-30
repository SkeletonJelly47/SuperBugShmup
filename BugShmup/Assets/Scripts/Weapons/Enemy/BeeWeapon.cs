using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeWeapon : EnemyWeapon
{
    public float bulletPerSecond;
    float FireInterval;
    float FireTimer = 0f;

    // Use this for initialization
    protected override void Start()
    {
        FireInterval = 1 / bulletPerSecond;
    }

    // Update is called once per frame
    protected override void Update()
    {
        FireTimer += Time.deltaTime;

        if(FireTimer > FireInterval)
        {
            Fire();
            FireTimer = 0f;
        }
    }

    protected override void Fire()
    {
        base.Fire();
    }
}