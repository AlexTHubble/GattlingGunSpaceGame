using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Managers
{
    //The point of this script is to hold all of the level stats so that other classes can get info from here
    public class LevelSetupScript : Singleton<LevelSetupScript>
    {

        [Header("Level effects")]

        [SerializeField]
        int playerHp = 10;
        [SerializeField]
        int bulletBounces = 1;
        [SerializeField]
        int playerDashCount = 100;
        [SerializeField]
        float dashCooldown = 0.5f;
        [SerializeField]
        float shootDelay = 0.02f;

        public int getPlayerHP()
        {
            return playerHp;
        }

        public int getBulletBounces()
        {
            return bulletBounces;
        }

        public int getPlayerDashCount()
        {
            return playerDashCount;
        }

        public float getDashCooldown()
        {
            return dashCooldown;
        }

        public float getShootDelay()
        {
            return shootDelay;
        }
    }
}
    
