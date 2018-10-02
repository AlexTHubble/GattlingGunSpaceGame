﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        int player1Hp = 10;
        int player2Hp = 10;
        [SerializeField]
        float godPeriod = 0.2f;

        bool p1GodEnabled = false;
        bool p2GodEnabled = false;
        float currentP1God = 0f;
        float currentP2God = 0f;

        private void Start()
        {
            player1Hp = Managers.LevelSetupScript.Instance.getPlayerHP();
            player2Hp = Managers.LevelSetupScript.Instance.getPlayerHP();
            Debug.Log("Player 1 health set to: " + player1Hp + " || Player 2 health set to: " + player2Hp);
            Managers.UiManager.Instance.updateP1Hp(player1Hp);
            Managers.UiManager.Instance.updateP2Hp(player2Hp);
        }

        private void Update()
        {
            handleGodPeriod();
        }

        public void reducePl1Hp(int hpLoss, bool selfHit)
        {
            if (!p1GodEnabled)
            {
                player1Hp -= hpLoss;
                currentP1God = Time.time + godPeriod;
                p1GodEnabled = true;
            }

            Managers.UiManager.Instance.updateP1Hp(player1Hp);
            Debug.Log("P1 took " + hpLoss + " damage, current hp: " + player1Hp);

            if(player1Hp <= 0)
            {
                if(selfHit)
                {
                    Managers.SceneManagerScript.Instance.nextLevel(false, false, true, false);
                }
                else
                {
                    Managers.SceneManagerScript.Instance.nextLevel(false, true, false, false);
                }
            }
        }

        public void reducePl2Hp(int hpLoss, bool selfHit)
        {
            if(!p2GodEnabled)
            {
                player2Hp -= hpLoss;
                currentP2God = Time.time + godPeriod;
                p2GodEnabled = true;
            }

            Managers.UiManager.Instance.updateP2Hp(player2Hp);
            Debug.Log("P1 took " + hpLoss + " damage, current hp: " + player1Hp);

            if (player2Hp <= 0)
            {
                if (selfHit)
                {
                    Managers.SceneManagerScript.Instance.nextLevel(false, false, false, true);
                }
                else
                {
                    Managers.SceneManagerScript.Instance.nextLevel(true, false, false, false);
                }
            }
        }

        private void handleGodPeriod()
        {
            if(p1GodEnabled && currentP1God <= Time.time)
            {
                p1GodEnabled = false;
            }
            if(p2GodEnabled && currentP2God <= Time.time)
            {
                p2GodEnabled = false;
            }
        }

    }
}

