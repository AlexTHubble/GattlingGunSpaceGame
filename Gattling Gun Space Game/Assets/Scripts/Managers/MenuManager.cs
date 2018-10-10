using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MenuManager : MonoBehaviour {
    private Player player1;
    private Player player2;

    [SerializeField]
    bool mainMenu = false;
    // Use this for initialization
    void Start ()
    {
        player1 = ReInput.players.GetPlayer("Player0");
        player2 = ReInput.players.GetPlayer("Player1");

        if(player1 == null)
        {
            Debug.Log("Player1 is null");
        }

        if(player2 == null)
        {
            Debug.Log("Player2 is null");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        moveToNextMenu();
	}

    void moveToNextMenu()
    {
        //Debug.Log("Update working");

        if (player1.GetButtonDown("Dash") || player2.GetButtonDown("Dash"))
        {
            Debug.Log("Detecting input correctly");

            if(!mainMenu)
            {
                Managers.SceneManagerScript.Instance.resetScores();
                Managers.SceneManagerScript.Instance.nextLevel();
            }
            else
            {
                Debug.Log("Menu if working...");
                //mainMenu = false;
                Managers.SceneManagerScript.Instance.moveToHowToPlay();
            }

        }
    }

}
