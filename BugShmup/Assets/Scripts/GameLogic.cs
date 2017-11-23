using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]

public class GameLogic : MonoBehaviour
{
    //public GameObject Player;
    public static PlayerController player;
    public int score;

    //Boundary variables
    BoxCollider bulletKill;
    [SerializeField] GameObject BG;
    [SerializeField] float boxHeight;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        score = 0;
        Debug.Log(Camera.main);
        Debug.Log("My name is " + gameObject.name, gameObject);
        bulletKill = gameObject.GetComponent<BoxCollider>() as BoxCollider;
        Debug.Log("my box collider is " + bulletKill);
        CreateBoundaries();
        //BG = GameObject.FindGameObjectWithTag("BG");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SaveCurrentScore(score);
            SceneManager.LoadScene("PrototypeLeaderboard");
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
    }

    private void SaveCurrentScore(int value)
    {
        //Save current score to PlayerPrefs so it can be used in another scene
        PlayerPrefs.SetInt("currentScore", value);
    }

    void CreateBoundaries()
    {
        //Get box collider
        bulletKill = gameObject.GetComponent<BoxCollider>();
        //Calulate distance from BG to camera
        float Ydist = Vector3.Distance(BG.transform.position, Camera.main.transform.position);
        //Calculate x and z size for bulletKill trigger
        float frustumHeight = 2.0f * Ydist * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustumHeight * Camera.main.aspect;

        //Set size,     Size.set doesn't work ¯\_(ツ)_/¯ fuck unity
        bulletKill.size = new Vector3(frustumWidth, boxHeight, frustumHeight);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerProjectile" || other.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }
    }
}
