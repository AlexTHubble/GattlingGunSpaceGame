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
        Text score;

        // Use this for initialization
        void Start()
        {
            p1Health = GameObject.Find("P1HP TMP").GetComponent<TextMeshPro>();
            p2Health = GameObject.Find("P2HP TMP").GetComponent<TextMeshPro>();
            //p1Health = GameObject.Find("p1Hp Text").GetComponent<Text>();
            //p2Health = GameObject.Find("p2Hp Text").GetComponent<Text>();
            score = GameObject.Find("Score Text").GetComponent<Text>();
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
    }
}
