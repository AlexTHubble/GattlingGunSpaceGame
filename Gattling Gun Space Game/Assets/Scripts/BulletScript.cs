using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [Header("Bullet properties")]
    [SerializeField]
    int bulletSpeed = 0;
    [SerializeField]
    int maxBounces = 0;
    [SerializeField]
    float maxVelocity = 0;

    int bounceCount = 0;
    Rigidbody2D bulletRidgedBody;


	// Use this for initialization
	void Start ()
    {
        maxBounces = Managers.LevelSetupScript.Instance.getBulletBounces();
        gameObject.transform.parent = null;
        //Vector3 dir = new Vector3(0f, 0f, 0f);
        bulletRidgedBody = gameObject.GetComponent<Rigidbody2D>(); //Gets the ridged body of the bullet
        bulletRidgedBody.AddForce(gameObject.transform.up * bulletSpeed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckMaxVelocity();
        faceVelocityDirection();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if(collision.gameObject.tag == "Wall")
        {
            bounceCount++;

            if (bounceCount > maxBounces)
                Destroy(gameObject);

            Managers.SoundManagerScript.Instance.playFXWithDelay("OnHit");
        }
        if (collision.gameObject.tag == "P1") //If it hits a player part
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "P2") //If it hits a player part
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone" || collision.tag == "P1" || collision.tag == "P2")
        {
            Destroy(gameObject);
        }
    }

    //Checks current velocity of the bullet, if it's higher than the max it slow it down
    void CheckMaxVelocity()
    {
        if(bulletRidgedBody.velocity[1] < maxVelocity)
        {
            bulletRidgedBody.AddForce(gameObject.transform.forward * bulletSpeed);
        }
    }

    void faceVelocityDirection()
    {
        Vector2 velocity = bulletRidgedBody.velocity;

        float angle = (Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg) - 90;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
