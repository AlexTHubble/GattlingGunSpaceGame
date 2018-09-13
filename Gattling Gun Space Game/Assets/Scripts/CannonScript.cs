using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

    GameObject bulletPrefab = null;
    public float shootDelay = 0f;
    bool delayInititated = false;
    float currentDelay = 0f;

    List<GameObject> spawnPoints = new List<GameObject>();
    int currentSpawnPoint = 0;

    // Use this for initialization
    void Start ()
    {
        bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        getBulletSpawns();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ShootBullet();
	}

    //void shootBullet()
    //{
    //    if(Input.GetButton("Fire"))
    //    {
    //        Debug.Log("Player shoot");
    //        Instantiate(bulletPrefab, gameObject.transform);
    //    }

    //}

    void ShootBullet()
    {
        if (Input.GetButton("Fire"))
            if (Input.GetButton("Fire") && !delayInititated)
            {
                Debug.Log("Player shoot");
                Debug.Log("Current spawnpoint: " + currentSpawnPoint);
                Instantiate(bulletPrefab, spawnPoints[currentSpawnPoint].gameObject.transform.position, spawnPoints[currentSpawnPoint].gameObject.transform.rotation);
                delayInititated = true;
                currentDelay = Time.time + shootDelay;

                currentSpawnPoint++;
                if (currentSpawnPoint > spawnPoints.Count - 1)
                {
                    currentSpawnPoint = 0;
                }
            }

        if (delayInititated && currentDelay <= Time.time)
        {
            delayInititated = false;
        }

    }

    void getBulletSpawns()
    {
        spawnPoints.Add(GameObject.Find("BulletSpawn1"));
        spawnPoints.Add(GameObject.Find("BulletSpawn2"));
        spawnPoints.Add(GameObject.Find("BulletSpawn3"));
        spawnPoints.Add(GameObject.Find("BulletSpawn4"));
    }
}
