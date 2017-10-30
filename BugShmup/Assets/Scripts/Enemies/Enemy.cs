using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    protected bool alive;
    BoxCollider collider;

    protected int Health
    {
        get
        {
            return health;
        }

        set
        {
            //dead
            health = value;
            if (health < 0)
            {
                Debug.Log("Dead");
                alive = false;
                DestroySelf();
            }
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}