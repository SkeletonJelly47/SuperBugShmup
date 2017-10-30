using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    Vector3 direction;
    [SerializeField] float moveSpeed;

    protected override void Start()
    {
        //Check if enemy rotation isn't 180 degrees
        if(transform.rotation.eulerAngles.y != 180)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
