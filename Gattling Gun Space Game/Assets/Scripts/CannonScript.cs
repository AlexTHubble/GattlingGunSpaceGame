using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    GameObject bulletPrefab = null;
    public float shootDelay = 0f;
    bool delayInititated = false;
    float currentDelay = 0f;

	// Use this for initialization
	void Start ()
    {
        bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        shootBullet();
	}

    void shootBullet()
    {
        if(Input.GetButton("Fire") && !delayInititated)
        {
            Debug.Log("Player shoot");
            Instantiate(bulletPrefab, gameObject.transform);
            delayInititated = true;
            currentDelay = Time.time + shootDelay;
        }

        if(delayInititated && currentDelay <= Time.time)
        {
            delayInititated = false;
        }

    }
}
