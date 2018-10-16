using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class SheildScript : MonoBehaviour {

    Player player;
    bool isShooting = false;
    //bool shooting = false;
    PolygonCollider2D collider;
    SpriteRenderer renderer;

    // Use this for initialization
    void Start ()
    {
        collider = gameObject.GetComponent<PolygonCollider2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        switch (tag)
        {
            case "P1":
                player = ReInput.players.GetPlayer("Player1");
                break;
            case "P2":
                player = ReInput.players.GetPlayer("Player0");
                break;
            default:
                break;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        handleInput();
	}

    void handleInput()
    {
        bool isShootingLocked = false;

        switch(tag)
        {
            case "P1":
                isShootingLocked = Managers.PlayerManager.Instance.testForP1LockShooting();
                break;
            case "P2":
                isShootingLocked = Managers.PlayerManager.Instance.testForP2LockShooting();
                break;
            default:
                break;
        }

        if (player.GetButtonDown("ShootGun"))
        {
            isShooting = true;
        }
        if (player.GetButtonUp("ShootGun"))
        {
            isShooting = false;
        }

        if(isShooting && !isShootingLocked)
        {
            disableSheild();
        }
        else
        {
            enableSheild();
        }
    }

    void disableSheild()
    {
        renderer.enabled = false;
        collider.enabled = false;
    }

    void enableSheild()
    {
        renderer.enabled = true;
        collider.enabled = true;
    }
}
