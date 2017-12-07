using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] GameObject SpawnObj;
    public GameObject PlayerProjectileSuperShot;

    public string FireKey = "Fire1";
    float timer;
    float fireInterval;
    public float BulletPerSecond;

    // Use this for initialization
    public virtual void Start()
    { 
        fireInterval = 1 / BulletPerSecond;
        timer = fireInterval;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (timer >= fireInterval)
        {
            if (Input.GetButton(FireKey))
            {
                Instantiate(SpawnObj, transform.position, transform.rotation);
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

        if (Input.GetButtonDown("Fire2"))
        {
            if (PlayerController.energy == 100)
            {
                PlayerController.energy = 0;
                Instantiate(PlayerProjectileSuperShot, transform.position, transform.rotation);
            }
        }
    }
}
