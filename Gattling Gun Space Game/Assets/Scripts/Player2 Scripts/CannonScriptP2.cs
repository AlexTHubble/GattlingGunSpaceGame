using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CannonScriptP2 : MonoBehaviour {

    GameObject bulletPrefab = null;
    public float shootDelay = 0f;
    bool delayInititated = false;
    float currentDelay = 0f;
    Player player;

    List<GameObject> spawnPoints = new List<GameObject>();
    int currentSpawnPoint = 0;

    // Use this for initialization
    void Start ()
    {
        bulletPrefab = Resources.Load("Prefabs/BulletP2") as GameObject;
        getBulletSpawns();
        player = ReInput.players.GetPlayer("Player0");
    }
	
	// Update is called once per frame
	void Update ()
    {
        ShootBullet();
	}

    void ShootBullet()
    {
        if (player.GetButtonSinglePressHold("ShootGun"))
            if (!delayInititated)
            {
                Debug.Log("Player2 shoot");
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
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn1").gameObject);
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn2").gameObject);
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn3").gameObject);
        spawnPoints.Add(gameObject.transform.Find("BulletSpawn4").gameObject);
    }
}
