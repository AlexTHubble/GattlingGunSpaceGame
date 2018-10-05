using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneManagerScript : Singleton<SceneManagerScript>
    {

        [Header("Level names")]

        [SerializeField]
        List<string> levelNames = new List<string>();
        [SerializeField]
        List<string> menuSceneNames = new List<string>();

        [Header("Global game variables")]
        [SerializeField]
        int scoreToWin = 5;

        [SerializeField]
        int scoreToLose = -3;

        int p1Score = 0;
        int p2Score = 0;
        bool gameOver = false;

        private void OnLevelWasLoaded(int level)
        {
            gameOver = false;
        }

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(this);   
        }

        private void Update()
        {
            Managers.UiManager.Instance.updateScore(p1Score, p2Score);
        }

        public void nextLevel()
        {
            Debug.Log("NEXT LEVEL");

            //Loads the next level if no player is victorious
            if (p1Score < scoreToWin && p2Score < scoreToWin)
            {
                int nextLevelId = Random.Range(0, levelNames.Count - 1);
                SceneManager.LoadScene(levelNames[nextLevelId]);
            }

            //Handles a player winning the game (possitive points)
            if (p1Score >= scoreToWin)
            {
                SceneManager.LoadScene(menuSceneNames[0]);
            }
            else if (p2Score >= scoreToWin)
            {
                SceneManager.LoadScene(menuSceneNames[1]);
            }

            //Handles a player losing the game (negative points)
            if (p1Score <= scoreToLose)
            {
                SceneManager.LoadScene(menuSceneNames[1]);
            }
            else if (p2Score <= scoreToLose)
            {
                SceneManager.LoadScene(menuSceneNames[0]);
            }
        }

        public void updateScore(bool p1Victorious, bool p2Victorious, bool p1SelfHit, bool p2SelfHit)
        {
            if (p1Victorious && gameOver == false)
            {
                p1Score++;
                gameOver = true;
            }
            if (p1SelfHit && gameOver == false)
            {
                p1Score--;
                gameOver = true;
            }
            if (p2Victorious && gameOver == false)
            {
                p2Score++;
                gameOver = true;
            }
            if (p2SelfHit && gameOver == false)
            {
                p2Score--;
                gameOver = true;
            }

            Debug.Log("Current score: " + p1Score + " - " + p2Score);

           
        }

    }
}

