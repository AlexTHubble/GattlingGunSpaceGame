using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {
    [Header("Player Movment")]
    public int dashPower = 0; //Movment speed of the player
    public float rotationSpeed = 0f;
    public int maxDashes = 0;
    public float dashCooldown = 0f;

    [Header("Internal Variables")]
    Rigidbody2D playerRidgedBody;
    float xDir = 0f;
    float yDir = 0f;
    int currentDashs = 0;
    float currentCooldown = 0f;
    bool cooldownInitiated = false;

	// Use this for initialization
	void Start ()
    {
        playerRidgedBody = gameObject.GetComponent<Rigidbody2D>(); //Gets the ridgedbody componant on start
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetAimDirection();
        PlayerRotation();
        Dash();
        //Debug.Log("Time.time" + Time.time);
    }

    //Gets the horizontal and verticle axis
    void GetAimDirection()
    {
        xDir = Input.GetAxis("Horizontal");
        yDir = Input.GetAxis("Vertical");
    }

    //Dashs the player when the x button is hit
    //NOTE: Requires PS4 controller to be plugged in for functionality
    void Dash()
    {
        //If the X key is pressed dash the player
        if(Input.GetButton("Dash") && !cooldownInitiated) 
        {
            if(currentDashs < maxDashes)
            {
                Vector3 movment = new Vector3(xDir, yDir, 0f); //Vector with the movment direction
                playerRidgedBody.AddForce(movment * dashPower); //Adds the force to the player
                Debug.Log("Player dashed at " + dashPower + " power in direction " + movment);
                currentDashs++;
                cooldownInitiated = true;
                currentCooldown = Time.time + dashCooldown;
            }
        }

        if(cooldownInitiated && currentCooldown <= Time.time)
        {
            cooldownInitiated = false;
        }
    }

    //Rotates the player based on horizontal and vertical axis
    //NOTE: Requires PS4 controller to be plugged in for functionality
    void PlayerRotation()
    {
        if((Input.GetAxis("Vertical") != 0f && Input.GetAxis("Horizontal") != 0f))
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI) - 90);
        }


    }
}
