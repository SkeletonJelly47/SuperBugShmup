using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTesting : MonoBehaviour
{
    public Text[] highScoreText;
    int[] highScoreValues;
    public int score;

    string[] highScoreNames;
    public InputField playerName;
    public Button button;

    private void Start()
    {
        highScoreValues = new int[highScoreText.Length];
        highScoreNames = new string[highScoreText.Length];
        //Load highscores and names from PlayerPrefs to an array
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreValues[i] = PlayerPrefs.GetInt("highScoreValues" + i);
            highScoreNames[i] = PlayerPrefs.GetString("highScoreNames" + i);
        }
        DrawScores();   
 
    }
    private void SaveScores()
    {   //Set values to PlayerPrefs from an array
        for (int i = 0; i < highScoreText.Length; i++)
        {
            PlayerPrefs.SetInt("highScoreValues" + i, highScoreValues[i]);
            PlayerPrefs.SetString("highScoreNames" + i, highScoreNames[i]);
        }
    }
    private void DrawScores()
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = highScoreNames[i] + " : " + highScoreValues[i].ToString();
        }
    }
    public void CheckIfHighScore(int value, string playerName)
    {
        value = PlayerPrefs.GetInt("currentScore", value);
        for (int i = 0; i < highScoreText.Length; i++)
        {   //Check if score is higher than any score on the highscore list
            if (value > highScoreValues[i])
            {   //Put score in the right spot on the list and shift every value under it down by one 
                for (int x = highScoreText.Length - 1; x > i; x--)
                {
                    highScoreValues[x] = highScoreValues[x - 1];
                    highScoreNames[x] = highScoreNames[x - 1];
                }

                highScoreValues[i] = value;
                highScoreNames[i] = playerName;
                DrawScores();
                SaveScores();
                break;
            }
        }
    }
    public void ButtonScript()
    {

       if (playerName.text == "")
        {
            Debug.Log("No name entered!");
        }
       else
        {
            CheckIfHighScore(score, playerName.text);
            button.interactable = false;
        }
    }
}
