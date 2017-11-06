using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHomingBullet : Bullet
{
    Transform target;
    Rigidbody rb;
    public float rotationSpeed;
    public float followTime;
    float timer;
    // Use this for initialization
    protected override void Start()
    {   
        ColliderTag = "Player";
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        timer += Time.deltaTime;
        if (timer > followTime)
        {
            base.Update();
        }
        else
        {
            base.Update();
            var rotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == ColliderTag)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            DestroySelf();
        }
    }
}
