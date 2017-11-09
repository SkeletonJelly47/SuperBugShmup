using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    public int energyAmount;
    [SerializeField]
    float speed;
    Vector3 dir;
    // Use this for initialization
    void Start()
    {
        dir = Vector3.back;
    }
    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            GameLogic.player.Energy += energyAmount;
            Destroy(gameObject);
        }
    }

}
