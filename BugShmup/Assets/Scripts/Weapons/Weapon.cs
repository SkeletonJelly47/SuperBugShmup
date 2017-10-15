using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    [SerializeField] GameObject SpawnObj;
    GameObject masterObject;
    public string FireKey = "Fire1";
    float timer;
    float fireInterval;
    public float BulletPerSecond;

    // Use this for initialization
    public virtual void Start()
    { 
        fireInterval = 1 / BulletPerSecond;
        timer = fireInterval;
        masterObject = GameObject.FindGameObjectWithTag("MasterObject");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (timer >= fireInterval)
        {
            if (Input.GetButton(FireKey))
            {
                Instantiate(SpawnObj, transform.position, transform.rotation, masterObject.transform);
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
