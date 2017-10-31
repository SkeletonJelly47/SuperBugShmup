using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}

    public bool isNewGame;
    public bool isSettings;
    public bool isQuit;

    void OnMouseUp()
    {
        if (isNewGame)
        {
            Application.LoadLevel(1);
        }

        if (isQuit)
        {
            Application.Quit();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
