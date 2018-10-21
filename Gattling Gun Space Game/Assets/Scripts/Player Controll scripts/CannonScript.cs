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

    [SerializeField]
    int clipSize = 100;

    int currentClip = 0;

    [SerializeField]
    float reloadTime = 1f;

    bool reloading = false;
    float currentReloadTime = 0f;

    //bool controllsLocked = false;
    

    List<GameObject> spawnPoints = new List<GameObject>();
    int currentSpawnPoint = 0;
    Player player;

    // Use this for initialization
    void Start ()
    {
        currentClip = clipSize;
        Managers.UiManager.Instance.setUpAmmoBar(gameObject.tag, clipSize);

        shootDelay = Managers.LevelSetupScript.Instance.getShootDelay();

        //Sets up stuff based on what player the cannon is tagged to
        switch (tag)
        {
            case "P1":
                bulletPrefab = Resources.Load("Prefabs/BulletP1") as GameObject;
                player = ReInput.players.GetPlayer("Player1");
                Debug.Log("Cannon: " + gameObject.name + " has been set to player 1");

                break;
            case "P2":
                bulletPrefab = Resources.Load("Prefabs/BulletP2 1") as GameObject;
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

        if(!Managers.PlayerManager.Instance.testForLockedControlls())
        {
            handleReloading();
            ShootBullet();
        }

    }


    void ShootBullet()
    {
        bool isShootingLocked = false;

        //Tests to see if the shooting has been locked externaly by the playerManager
        switch (tag)
        {
            case "P1":
                isShootingLocked = Managers.PlayerManager.Instance.testForP1LockShooting();
                break;
            case "P2":
                isShootingLocked = Managers.PlayerManager.Instance.testForP2LockShooting();
                break;
            default:
                break;
        }


        if (player.GetButtonDown("ShootGun") && reloading == false)
        {
            shooting = true;
        }
        if (player.GetButtonUp("ShootGun"))
        {
            shooting = false;
        }


        if (shooting && !reloading && !isShootingLocked)
            if (!delayInititated)
            {
                //Turns off godmode
                switch(gameObject.tag)
                {
                    case "P1":
                        Managers.PlayerManager.Instance.setP1GodMode(false);
                        break;
                    case "P2":
                        Managers.PlayerManager.Instance.setP2GodMode(false);
                        break;
                }

                //Spawns bullet and reduces clip count
                Instantiate(bulletPrefab, spawnPoints[currentSpawnPoint].gameObject.transform.position, spawnPoints[currentSpawnPoint].gameObject.transform.rotation);
                currentClip--;

                //Update's UI
                Managers.UiManager.Instance.updateAmmoBar(gameObject.tag, currentClip);

                //Sets up delay
                delayInititated = true;
                currentDelay = Time.time + shootDelay;

                //Sets up next spawnpoint
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

    void handleReloading()
    {
        if(currentClip <= 0 && reloading == false)
        {
            //Debug.Log("RELOADING");
            //shooting = false;
            reloading = true;

            currentReloadTime = Time.time + reloadTime;
        }

        if(player.GetButtonDown("Reload"))
        {
            //shooting = false;
            reloading = true;

            currentReloadTime = Time.time + reloadTime;
        }

        if(reloading && currentReloadTime <= Time.time)
        {
            //Debug.Log("Done Reloading");
            reload();
            
        }
    }

    void reload() //Reloads the weapon
    {
        currentClip = clipSize;
        Managers.UiManager.Instance.updateAmmoBar(gameObject.tag, currentClip);
        reloading = false;
    }
}
