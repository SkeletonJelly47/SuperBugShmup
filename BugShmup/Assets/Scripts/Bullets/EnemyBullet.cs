using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    
    // Use this for initialization
    protected override void Start()
    {
        ColliderTag = "Player";
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
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            DestroySelf();
        }
    }
}
