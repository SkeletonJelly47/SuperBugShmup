using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected int damage;
    protected string ColliderTag;

    // Use this for initialization
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == ColliderTag)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
