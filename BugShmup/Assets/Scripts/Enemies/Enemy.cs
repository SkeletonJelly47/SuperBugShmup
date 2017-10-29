using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private int health;
    protected bool alive;

    protected int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            if (health < 0)
            {
                alive = false;
                DestroySelf();
            }
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}