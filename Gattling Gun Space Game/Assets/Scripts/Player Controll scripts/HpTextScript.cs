using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpTextScript : MonoBehaviour
{

    Transform playerTransform;
    Transform thisTransform;

	// Use this for initialization
	void Start ()
    {
        if (gameObject.tag == "P1")
            playerTransform = GameObject.Find("Player 1").transform;
        else if (gameObject.tag == "P2")
            playerTransform = GameObject.Find("Player 2").transform;
        else
            Debug.Log("Error no applicable tag on: " + gameObject.name);

        thisTransform = gameObject.transform;
        gameObject.transform.parent = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveToPlayer();
	}

    void moveToPlayer()
    {
        Vector2 newPosition = playerTransform.position;

        newPosition.y += 1.5f;

        thisTransform.position = newPosition;
        
    }
}
