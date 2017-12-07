using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShotScript : Bullet
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        //Make this smarter
        ColliderTag = "Enemy";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == ColliderTag)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        if(collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
    }
}