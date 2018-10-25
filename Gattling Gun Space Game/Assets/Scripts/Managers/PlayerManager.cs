using System.Collections;
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

        [SerializeField]
        float timebeforeExplosion = 1f;

        bool explosionInitiated = false;
        float currentExploadDelay = 0;

        string playerToExpload;

        bool p1GodEnabled = false;
        bool p2GodEnabled = false;
        float currentP1God = 0f;
        float currentP2God = 0f;
        bool lockControlls = false;

        bool p1LockShooting = false;
        bool p2LockShooting = false;
        bool p1LockShootingOver = false;
        bool p2LockShootingOver = false;

        float p1CurrentLockShootTime = 0f;
        float p2CurrentLockShootTime = 0f;
        [SerializeField]
        float lockShootTime = .5f;

        bool gameOverPause = false;
        bool gameOverInitiated = false;

        [SerializeField]
        float gameOverPauseTime = 3f;

        [SerializeField]
        float godModeAlphaValue = 111f;
        float defaultPlayerAplha;

        float currentGameOverPauseTime = 0f;

        GameObject player1;
        SpriteRenderer player1SpriteRenderer;
        GameObject player1Sheild;
        GameObject player1BackSheild;
        PolygonCollider2D player1BackSheildPolyCollider;
        SpriteRenderer player1BackSheildSpriteRenderer;

        GameObject player2;
        SpriteRenderer player2SpriteRenderer;
        GameObject player2Sheild;
        GameObject player2BackSheild;
        PolygonCollider2D player2BackSheildPolyCollider;
        SpriteRenderer player2BackSheildSpriteRenderer;

        ParticleSystem p1OnHitFX;
        ParticleSystem p2OnHitFX;
        ParticleSystem p1OnDeathFX;
        ParticleSystem p2OnDeathFX;

        CameraZoomToPlayerScript cameraHolder;

        private void Start()
        {
            //Finds player gameobjects
            player1 = GameObject.Find("Player");
            player2 = GameObject.Find("Player 2");

            //Gets the sprite renderers from players
            player1SpriteRenderer = player1.GetComponent<SpriteRenderer>();
            player2SpriteRenderer = player2.GetComponent<SpriteRenderer>();

            defaultPlayerAplha = player1SpriteRenderer.color.a;

            //Gets the backSheild from the player gameobjects
            player1BackSheild = player1.transform.Find("BackSheild").gameObject;
            player1BackSheildPolyCollider = player1BackSheild.GetComponent<PolygonCollider2D>();
            player1BackSheildSpriteRenderer = player1BackSheild.GetComponent<SpriteRenderer>();

            player2BackSheild = player2.transform.Find("BackSheild").gameObject;
            player2BackSheildPolyCollider = player2BackSheild.GetComponent<PolygonCollider2D>();
            player2BackSheildSpriteRenderer = player2BackSheild.GetComponent<SpriteRenderer>();

            //Gets the sheilds for the players
            player1Sheild = player1.transform.Find("FrontSheild").gameObject;
            player2Sheild = player2.transform.Find("FrontSheild").gameObject;

            //Sets up the players hp
            player1Hp = Managers.LevelSetupScript.Instance.getPlayerHP();
            player2Hp = Managers.LevelSetupScript.Instance.getPlayerHP();
            Debug.Log("Player 1 health set to: " + player1Hp + " || Player 2 health set to: " + player2Hp);
            Managers.UiManager.Instance.updateP1Hp(player1Hp);
            Managers.UiManager.Instance.updateP2Hp(player2Hp);

            //Gets particle fx's 
            p1OnHitFX = GameObject.Find("Player1 Hit FX").GetComponent<ParticleSystem>();
            p2OnHitFX = GameObject.Find("Player2 Hit FX").GetComponent<ParticleSystem>();

            p1OnDeathFX = GameObject.Find("Player1 Die FX").GetComponent<ParticleSystem>();
            p2OnDeathFX = GameObject.Find("Player2 Die FX").GetComponent<ParticleSystem>();

            cameraHolder = GameObject.Find("CameraHolder").GetComponent<CameraZoomToPlayerScript>();
        }

        private void OnLevelWasLoaded(int level)
        {
            lockControlls = false;
            gameOverInitiated = false;
            gameOverPause = false;
        }

        private void Update()
        {
            handleGodPeriod();
            handleGameoverPause();
        }

        public void reducePl1Hp(int hpLoss, bool selfHit)
        {
            if(!lockControlls)
            {
                if (!p1GodEnabled)
                {
                    Managers.SoundManagerScript.Instance.playRandomBatmanFX();

                    p1OnHitFX.Play();

                    player1Hp -= hpLoss;
                    currentP1God = Time.time + godPeriod;
                    p1GodEnabled = true;
                }

                Managers.UiManager.Instance.updateP1Hp(player1Hp);
                Debug.Log("P1 took " + hpLoss + " damage, current hp: " + player1Hp);

                if (player1Hp <= 0)
                {
                    playerToExpload = "P1";
                    currentExploadDelay = Time.time + timebeforeExplosion;
                    explosionInitiated = true;


                    //p1OnDeathFX.Play();

                    if (selfHit)
                    {
                        cameraHolder.startMoving(player1.GetComponent<Transform>().position);

                        Managers.SceneManagerScript.Instance.updateScore(false, false, true, false);
                        gameOverPause = true;
                        lockControlls = true;
                        Managers.UiManager.Instance.displaySelfKill("P1");
                        currentGameOverPauseTime = Time.time + gameOverPauseTime;
                    }
                    else
                    {
                        cameraHolder.startMoving(player1.GetComponent<Transform>().position);

                        Managers.SceneManagerScript.Instance.updateScore(false, true, false, false);
                        gameOverPause = true;
                        lockControlls = true;
                        Managers.UiManager.Instance.displayWinText("P2");
                        currentGameOverPauseTime = Time.time + gameOverPauseTime;
                    }
                }
            }
        }

        public void reducePl2Hp(int hpLoss, bool selfHit)
        {
            if(!lockControlls)
            {
                if (!p2GodEnabled)
                {
                    Managers.SoundManagerScript.Instance.playRandomBatmanFX();
                    p2OnHitFX.Play();
                    player2Hp -= hpLoss;
                    currentP2God = Time.time + godPeriod;
                    p2GodEnabled = true;
                }

                Managers.UiManager.Instance.updateP2Hp(player2Hp);
                Debug.Log("P1 took " + hpLoss + " damage, current hp: " + player1Hp);

                if (player2Hp <= 0)
                {
                    playerToExpload = "P2";
                    currentExploadDelay = Time.time + timebeforeExplosion;
                    explosionInitiated = true;

                    if (selfHit)
                    {
                        cameraHolder.startMoving(player2.GetComponent<Transform>().position);

                        Managers.SceneManagerScript.Instance.updateScore(false, false, false, true);
                        gameOverPause = true;
                        lockControlls = true;
                        Managers.UiManager.Instance.displaySelfKill("P2");
                        currentGameOverPauseTime = Time.time + gameOverPauseTime;
                    }
                    else
                    {
                        cameraHolder.startMoving(player2.GetComponent<Transform>().position);

                        Managers.SceneManagerScript.Instance.updateScore(true, false, false, false);
                        gameOverPause = true;
                        lockControlls = true;
                        Managers.UiManager.Instance.displayWinText("P1");
                        currentGameOverPauseTime = Time.time + gameOverPauseTime;

                    }
                }
            }
            
        }


        private void lockPlayerControlls()
        {
            lockControlls = true;
        }

        private void handleGameoverPause()
        {
            if(explosionInitiated == true && currentExploadDelay <= Time.time)
            {
                explosionInitiated = false;
                switch(playerToExpload)
                {
                    case "P1":
                        p1OnDeathFX.Play();
                        disablePlayer1();
                        //Destroy(player1);
                        break;
                    case "P2":
                        p2OnDeathFX.Play();
                        disablePlayer2();
                        //Destroy(player2);
                        break;
                    default:
                        Debug.Log("Error displaying explosion yo");
                        break;
                }
                

            }

            if(gameOverPause == true && currentGameOverPauseTime <= Time.time)
            {
                Managers.SceneManagerScript.Instance.nextLevel();
                //gameOverInitiated = true;
            }
        }


        //Enables godmode effects for player 1
        private void p1EnableGodModeEffects()
        {
            player1BackSheildPolyCollider.enabled = true;
            player1BackSheildSpriteRenderer.enabled = true;

            //Sets up the lock shooting
            if (!p1LockShooting && !p1LockShootingOver)
            {
                p1LockShooting = true;
                p1CurrentLockShootTime = Time.time + lockShootTime;
                Debug.Log("Shooting has been locked for player 1");

                Color newColor = player1SpriteRenderer.color;
                newColor.a = godModeAlphaValue;
                player1SpriteRenderer.color = newColor;
            }

            //If the time has run out for lockedShooting
            if (p1LockShooting && p1CurrentLockShootTime <= Time.time)
            {
                p1LockShootingOver = true;
                p1LockShooting = false;
                Debug.Log("Shooting has been unlocked for player1");

                Color newColor = player1SpriteRenderer.color;
                newColor.a = defaultPlayerAplha;
                player1SpriteRenderer.color = newColor;
            }
        }

        //Enables godmode effects for player 2
        private void p2EnableGodModeEffects()
        {
            player2BackSheildPolyCollider.enabled = true;
            player2BackSheildSpriteRenderer.enabled = true;

            //Sets up the lock shooting
            if (!p2LockShooting && !p2LockShootingOver)
            {
                p2LockShooting = true;
                p2CurrentLockShootTime = Time.time + lockShootTime;

                Debug.Log("Shooting has been locked for player 2");

                Color newColor = player2SpriteRenderer.color;
                newColor.a = godModeAlphaValue;
                player2SpriteRenderer.color = newColor;

            }

            //If the time has run out for lockedShooting
            if (p2LockShooting && p2CurrentLockShootTime <= Time.time)
            {
                p2LockShootingOver = true;
                p2LockShooting = false;
                Debug.Log("Shooting has been unlocked for player2");

                Color newColor = player2SpriteRenderer.color;
                newColor.a = defaultPlayerAplha;
                player2SpriteRenderer.color = newColor;
            }

        }

        //Dissables godmode effects for player 1
        private void p1DissableGodModeEffects()
        {
            player1BackSheildPolyCollider.enabled = false;
            player1BackSheildSpriteRenderer.enabled = false;
            p1LockShooting = false;
            p1LockShootingOver = false;

        }

        //Dissables godmode effects  for player 2
        private void p2DissableGodModeEffects()
        {
            player2BackSheildPolyCollider.enabled = false;
            player2BackSheildSpriteRenderer.enabled = false;
            p2LockShooting = false;
            p2LockShootingOver = false;

        }

        //Does the every frame checks for handling god period stuff
        private void handleGodPeriod()
        {

            //If the time has run out for god mode
            if (p1GodEnabled && currentP1God <= Time.time)
            {
                p1GodEnabled = false;
            }
            if (p2GodEnabled && currentP2God <= Time.time)
            {
                p2GodEnabled = false;
            }

            //If the godmode has been enabled...
            if (p1GodEnabled)
            {
                //Setup back sheild / effects for p1
                p1EnableGodModeEffects();
            }
            if(p2GodEnabled)
            {
                //Setup back sheild / effects for p2
                p2EnableGodModeEffects();
            }

            //If godmode has been disabled externaly
            if(!p1GodEnabled)
            {
                p1DissableGodModeEffects();
            }
            if(!p2GodEnabled)
            {
                p2DissableGodModeEffects();
            }
        }

        void disablePlayer1()
        {
            player1SpriteRenderer.enabled = false;
            Component[] spriteRenderes;

            spriteRenderes = player1.GetComponentsInChildren<SpriteRenderer>();

            foreach(SpriteRenderer renderer in spriteRenderes)
            {
                renderer.enabled = false;
            }

            Managers.UiManager.Instance.disableP1Hp();

            Managers.UiManager.Instance.disableP1AmmoBar();

            player1Sheild.SetActive(false);
            player1BackSheild.SetActive(false);
        }

        void disablePlayer2()
        {
            Component[] spriteRenderes;

            spriteRenderes = player2.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer renderer in spriteRenderes)
            {
                renderer.enabled = false;
            }

            Managers.UiManager.Instance.disableP2Hp();

            Managers.UiManager.Instance.disableP2AmmoBar();

            player2Sheild.SetActive(false);
            player2BackSheild.SetActive(false);
        }
   

        //-----------------------------------------------------------Setters and getters---------------------------------------------------------
        public bool testForP1GodMode()
        {
            return p1GodEnabled;
        }

        public bool testForP2GodMode()
        {
            return p2GodEnabled;
        }

        public void setP1GodMode(bool godMode)
        {
            p1GodEnabled = godMode;
        }

        public void setP2GodMode(bool godMode)
        {
            p2GodEnabled = godMode;
        }

        public bool testForP1LockShooting()
        {
            return p1LockShooting;
        }

        public bool testForP2LockShooting()
        {
            return p2LockShooting;
        }

        public bool testForLockedControlls()
        {
            return lockControlls;
        }

    }
}

