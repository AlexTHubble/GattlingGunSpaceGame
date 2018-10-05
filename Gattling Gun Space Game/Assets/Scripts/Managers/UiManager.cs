using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        TextMeshPro p1Health;
        TextMeshPro p2Health;

        TextMeshPro p1Win;
        TextMeshPro p2Win;
        TextMeshPro p1SelfKill;
        TextMeshPro p2SelfKill;

        Text score;

        Slider p1Ammo;
        Slider p2Ammo;

        bool player1Reloading = false;
        bool player2Reloading = false;

        // Use this for initialization
        void Start()
        {
            p1Win = GameObject.Find("P1 WinText TMP").GetComponent<TextMeshPro>();
            p2Win = GameObject.Find("P2 WinText TMP").GetComponent<TextMeshPro>();
            p1SelfKill = GameObject.Find("P1 SelfKill TMP").GetComponent<TextMeshPro>();
            p2SelfKill = GameObject.Find("P2 SelfKill TMP").GetComponent<TextMeshPro>();

            p1Ammo = GameObject.Find("ReloadBar P1").GetComponent<Slider>();
            p2Ammo = GameObject.Find("ReloadBar P2").GetComponent<Slider>();

            p1Health = GameObject.Find("P1HP TMP").GetComponent<TextMeshPro>();
            p2Health = GameObject.Find("P2HP TMP").GetComponent<TextMeshPro>();
            //p1Health = GameObject.Find("p1Hp Text").GetComponent<Text>();
            //p2Health = GameObject.Find("p2Hp Text").GetComponent<Text>();
            score = GameObject.Find("Score Text").GetComponent<Text>();
        }

        private void Update()
        {
            //reloadAnimation();
        }

        public void updateP1Hp(int hp)
        {
            if(p1Health == null)
            {
                p1Health = GameObject.Find("P1HP TMP").GetComponent<TextMeshPro>();
            }
            p1Health.text = hp.ToString();
        }

        public void updateP2Hp(int hp)
        {
            if(p2Health == null)
            {
                p2Health = GameObject.Find("P2HP TMP").GetComponent<TextMeshPro>();
            }

            p2Health.text = hp.ToString();
        }

        public void updateScore(int p1Score, int p2Score)
        {
            if(score == null)
            {
                score = GameObject.Find("Score Text").GetComponent<Text>();
            }

            if(p1Score < 0)
            {
                score.text = "(" + p1Score.ToString() + ")" + " - " + p2Score.ToString();
            }
            else if(p2Score < 0)
            {
                score.text = p1Score.ToString() + " - " + "(" + p2Score.ToString() + ")";
            }
            else if(p1Score < 0 && p2Score < 0)
            {
                score.text = "(" + p1Score.ToString() + ")" + " - " + "(" + p2Score.ToString() + ")";
            }
            else
            {
                score.text =  p1Score.ToString() + " - " + p2Score.ToString();
            }

            
        }

        //Updates the ammo bar
        public void updateAmmoBar(string player, int ammo)
        {
            if(player == "P1")
            {
                p1Ammo.value = ammo;
            }

            if(player == "P2")
            {
                p2Ammo.value = ammo;
            }
        }

        //Initial setup for the ammo bar
        public void setUpAmmoBar(string player, int maxAmmo)
        {
            if(p1Ammo == null || p2Ammo == null)
            {
                p1Ammo = GameObject.Find("ReloadBar P1").GetComponent<Slider>();
                p2Ammo = GameObject.Find("ReloadBar P2").GetComponent<Slider>();
            }

            if (player == "P1")
            {
                p1Ammo.maxValue = maxAmmo;
            }

            if (player == "P2")
            {
                p2Ammo.maxValue = maxAmmo;
            }
        }

        public void displayWinText(string player)
        {
            switch(player)
            {
                case "P1":
                    p1Win.GetComponent<MeshRenderer>().enabled = true;
                    break;
                case "P2":
                    p2Win.GetComponent<MeshRenderer>().enabled = true;
                    break;
                default:
                    break;
            }
        }

        public void displaySelfKill(string player)
        {
            switch (player)
            {
                case "P1":
                    p1SelfKill.GetComponent<MeshRenderer>().enabled = true;
                    break;
                case "P2":
                    p2SelfKill.GetComponent<MeshRenderer>().enabled = true;
                    break;
                default:
                    break;
            }
        }

        //Handles the reload animation
        void reloadAnimation()
        {

        }
    }
}
