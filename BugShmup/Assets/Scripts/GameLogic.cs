using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject MasterObject;
    public int score;
    private void Start()
    {
        score = 0;
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
}
