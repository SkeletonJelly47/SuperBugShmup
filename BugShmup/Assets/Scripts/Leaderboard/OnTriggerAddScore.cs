using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerAddScore : MonoBehaviour
{
    public int score;
    private GameLogic gameLogic;
    // Use this for initialization
    void Start()
    {
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            gameLogic.AddScore(score);
        }
    }
}
