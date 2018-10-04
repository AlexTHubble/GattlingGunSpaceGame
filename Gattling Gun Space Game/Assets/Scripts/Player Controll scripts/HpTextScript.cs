using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpTextScript : MonoBehaviour
{

    Transform playerTransform;
    Transform thisTransform;

    [SerializeField]
    Vector2 offSet = new Vector2(0.0f, 1.5f);

	// Use this for initialization
	void Start ()
    {
        if (gameObject.tag == "P1")
            playerTransform = GameObject.Find("Player").transform;
        else if (gameObject.tag == "P2")
            playerTransform = GameObject.Find("Player 2").transform;
        else
            Debug.Log("Error no applicable tag on: " + gameObject.name);

        thisTransform = gameObject.transform;
        gameObject.transform.SetParent(null);
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveToPlayer();
	}

    void moveToPlayer()
    {
        Vector2 newPosition = playerTransform.position;

        newPosition.x += offSet.x;
        newPosition.y += offSet.y;

        thisTransform.position = newPosition;
        
    }
}
