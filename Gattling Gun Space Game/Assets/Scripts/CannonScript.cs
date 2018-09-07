using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    GameObject bulletPrefab = null;

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
        if(Input.GetButton("Fire"))
        {
            Debug.Log("Player shoot");
            Instantiate(bulletPrefab, gameObject.transform);
        }

    }
}
