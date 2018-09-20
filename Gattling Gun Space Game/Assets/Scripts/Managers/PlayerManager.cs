using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public int player1Hp = 10;
        public int player2Hp = 10;

        private void Start()
        {
            Managers.UiManager.Instance.updateP1Hp(player1Hp);
            Managers.UiManager.Instance.updateP2Hp(player2Hp);
        }

        public void reducePl1Hp(int hpLoss, bool selfHit)
        {
            player1Hp -= hpLoss;
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
            player2Hp -= hpLoss;
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

    }
}

