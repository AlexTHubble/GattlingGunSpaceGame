using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneManagerScript : Singleton<SceneManagerScript>
    {

        [Header("Level names")]
        public List<string> levelNames = new List<string>();
        public List<string> menuSceneNames = new List<string>();

        [Header("Global game variables")]
        public int scoreToWin = 5;

        int p1Score = 0;
        int p2Score = 0;


        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);   
        }

        private void Update()
        {
            Managers.UiManager.Instance.updateScore(p1Score, p2Score);
        }

        public void nextLevel(bool p1Victorious, bool p2Victorious, bool p1SelfHit, bool p2SelfHit)
        {
            if (p1Victorious)
            {
                p1Score++;
            }
            if (p1SelfHit)
            {
                p1Score--;
            }
            if (p2Victorious)
            {
                p2Score++;
            }
            if (p2SelfHit)
            {
                p2Score--;
            }

            Debug.Log("Current score: " + p1Score + " - " + p2Score);

            //Loads the next level if no player is victorious
            if (p1Score < scoreToWin && p2Score < scoreToWin)
            {
                int nextLevelId = Random.Range(0, levelNames.Count - 1);
                SceneManager.LoadScene(levelNames[nextLevelId]);
            }

            if (p1Score < scoreToWin)
            {

            }
            else if (p2Score < scoreToWin)
            {

            }
        }

    }
}

