using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] float projectileSpeed;

    // Use this for initialization
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
}
