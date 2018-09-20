using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        Text p1Health;
        Text p2Health;
        Text score;

        // Use this for initialization
        void Start()
        {

            p1Health = GameObject.Find("p1Hp Text").GetComponent<Text>();
            p2Health = GameObject.Find("p2Hp Text").GetComponent<Text>();
            score = GameObject.Find("Score Text").GetComponent<Text>();
        }

        public void updateP1Hp(int hp)
        {
            if(p1Health == null)
            {
                p1Health = GameObject.Find("p1Hp Text").GetComponent<Text>();
            }
            p1Health.text = "P1 Hp: " + hp.ToString();
        }

        public void updateP2Hp(int hp)
        {
            if(p2Health == null)
            {
                p2Health = GameObject.Find("p2Hp Text").GetComponent<Text>();
            }

            p2Health.text = "P2 Hp: " + hp.ToString();
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
    }
}
