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
    [SerializeField] float killTriggerHeight;
    [SerializeField] float killTriggerPadding;
    [SerializeField] float boundaryXInset, boundaryZInset;
    float frustumHeight;
    float frustumWidth;

    //Static and non-static references
    public static GameLogic GL;
    public GameObject DieScreen;
    public GameObject PauseScreen;

    private void Start()
    {
        if (GL == null)
        {
            GL = this;
            Debug.Log(GL);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        CreateBoundaries();
        //Give boundaries to player
        SendBoundariesToPlayer();
        score = 0;
        bulletKill = gameObject.GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SaveCurrentScore(score);
            SceneManager.LoadScene("PrototypeLeaderboard");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
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
        float Ydist = (Camera.main.transform.position - transform.position).y;
        //Calculate x and z size for bulletKill trigger
        frustumHeight = 2.0f * Ydist * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight * Camera.main.aspect;

        //Set size,     Size.set doesn't work ¯\_(ツ)_/¯ fuck unity
        bulletKill.size = new Vector3(frustumWidth + killTriggerPadding, killTriggerHeight, frustumHeight + killTriggerPadding);
    }

    void SendBoundariesToPlayer()
    {
        //Send player the boundaries with insets applied, relative to the origin (GameLogic)
        player.ReceiveBoundaries(frustumWidth - boundaryXInset, frustumHeight - boundaryZInset, transform.position);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerProjectile" || other.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        DieScreen.SetActive(true);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnLevelWasLoaded()
    {
        Time.timeScale = 1f;
    }

    void TimeToggle()
    {
        if (player.Health > 0)
        {
            //Stop time
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
            }
            //Resume time
            else if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void PauseToggle()
    {
        if (player.Health > 0)
        {
            //Pausing
            if (PauseScreen.activeSelf == false)
            {
                PauseScreen.SetActive(true);
                TimeToggle();
            }
            //Unpausing
            else
            {
                PauseScreen.SetActive(false);
                TimeToggle();
            }

        }
    }
}
