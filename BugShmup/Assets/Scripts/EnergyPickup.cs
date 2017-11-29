using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    public int energyAmount;
    [SerializeField]
    float speed;
    [SerializeField]
    float followDistance;
    Transform player;
    Vector3 dir;
    float dist;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        dir = Vector3.back;
    }
    private void Update()
    {
        dist = Vector3.Distance(player.position, transform.position);
        if (dist <= followDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (dist <= followDistance - 0.5)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * 2 * Time.deltaTime);
            }
        }
        else
        {
            transform.position += dir * speed * Time.deltaTime;
        }
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
