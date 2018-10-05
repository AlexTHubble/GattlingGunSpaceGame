using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Player Movment")]

    Transform playerTransform;

    [SerializeField]
    float rotationSpeed = 0f;

    [SerializeField]
    int dashPower = 0; //Movment speed of the player

    int maxDashes = 0;
    float dashCooldown = 0f;

    [Header("Internal Variables")]
    Rigidbody2D playerRidgedBody;
    float xDir = 0f;
    float yDir = 0f;
    int currentDashs = 0;
    float currentCooldown = 0f;
    bool cooldownInitiated = false;
    private Player player;

    // Use this for initialization
    void Start()
    {
        maxDashes = Managers.LevelSetupScript.Instance.getPlayerDashCount();
        dashCooldown = Managers.LevelSetupScript.Instance.getDashCooldown();

        playerRidgedBody = gameObject.GetComponent<Rigidbody2D>(); //Gets the ridgedbody componant on start
        playerTransform = gameObject.transform;
        switch(tag)
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
    void Update()
    {
        if(!Managers.PlayerManager.Instance.testForLockedControlls())
        {
            GetAimDirection();
            PlayerRotation();
            Dash();
        }
        //Debug.Log("Time.time" + Time.time);
    }

    //Gets the horizontal and verticle axis
    void GetAimDirection()
    {
        xDir = player.GetAxis("MoveHorizontal");
        yDir = player.GetAxis("MoveVertical");
    }

    //Dashs the player when the x button is hit
    //NOTE: Requires PS4 controller to be plugged in for functionality
    void Dash()
    {
        //If the X key is pressed dash the player
        if (player.GetButtonDown("Dash") && !cooldownInitiated)
        {
            if (currentDashs < maxDashes)
            {
                Vector3 movment = new Vector3(xDir, yDir, 0f); //Vector with the movment direction
                playerRidgedBody.AddForce(movment * dashPower); //Adds the force to the player
                //Debug.Log("Player 2 dashed at " + dashPower + " power in direction " + movment);
                currentDashs++;
                cooldownInitiated = true;
                currentCooldown = Time.time + dashCooldown;
            }
        }

        if (cooldownInitiated && currentCooldown <= Time.time)
        {
            cooldownInitiated = false;
        }
    }

    //Rotates the player based on horizontal and vertical axis
    //NOTE: Requires PS4 controller to be plugged in for functionality
    void PlayerRotation()
    {
        if ((player.GetAxis("MoveHorizontal") != 0f && player.GetAxis("MoveVertical") != 0f))
        {
            playerTransform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(player.GetAxis("MoveVertical"), player.GetAxis("MoveHorizontal")) * 180 / Mathf.PI) - 90);
        }


    }
}
