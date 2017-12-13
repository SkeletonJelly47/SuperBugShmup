using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakeDamageScript : MonoBehaviour
{
    GameObject boss;
    // Use this for initialization
    void Start()
    {
        boss = GameObject.Find("SpiderBoss");
        Debug.Log("FUCK PERFORMANCE");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        boss.gameObject.GetComponent<BossScript>().TakeDamage(damage);
    }
}
