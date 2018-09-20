using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [Header("Bullet properties")]
    public int bulletSpeed = 0;
    public int maxBounces = 0;
    public float maxVelocity = 0;

    int bounceCount = 0;
    Rigidbody2D bulletRidgedBody;


	// Use this for initialization
	void Start ()
    {
        gameObject.transform.parent = null;
        //Vector3 dir = new Vector3(0f, 0f, 0f);
        bulletRidgedBody = gameObject.GetComponent<Rigidbody2D>(); //Gets the ridged body of the bullet
        bulletRidgedBody.AddForce(gameObject.transform.up * bulletSpeed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckMaxVelocity();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            bounceCount++;

            if (bounceCount > maxBounces)
                Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
            Destroy(gameObject);
        }
    }

    //Checks current velocity of the bullet, if it's higher than the max it slow it down
    void CheckMaxVelocity()
    {
        if(bulletRidgedBody.velocity[1] != maxVelocity)
        {
            //bulletRidgedBody.velocity = new Vector2(0f, maxVelocity);
        }
    }
}
