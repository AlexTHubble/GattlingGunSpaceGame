using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CannonScript : MonoBehaviour {

    GameObject bulletPrefab = null;
    float shootDelay = 0f;
    bool delayInititated = false;
    float currentDelay = 0f;
    bool shooting = false;

    List<GameObject> spawnPoints = new List<GameObject>();
    int currentSpawnPoint = 0;
    Player player;

    // Use this for initialization
    void Start ()
    {
        shootDelay = Managers.LevelSetupScript.Instance.getShootDelay();
        switch (tag)
        {
            case "P1":
                bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
                player = ReInput.players.GetPlayer("Player1");
                Debug.Log("Cannon: " + gameObject.name + " has been set to player 1");

                break;
            case "P2":
                bulletPrefab = Resources.Load("Prefabs/BulletP2") as GameObject;
                player = ReInput.players.GetPlayer("Player0");
                Debug.Log("Cannon: " + gameObject.name + " has been set to player 0");
                break;
            default:
                break;
        }

        
        getBulletSpawns();

    }
	
	// Update is called once per frame
	void Update ()
    {
        ShootBullet();
	}


    void ShootBullet()
    {
        if (player.GetButtonDown("ShootGun"))
        {
            shooting = true;
        }
        if (player.GetButtonUp("ShootGun"))
        {
            shooting = false;
        }


        if (shooting)
            if (!delayInititated)
            {
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
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn1").gameObject);
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn2").gameObject);
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn3").gameObject);
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn4").gameObject);
    }
}
