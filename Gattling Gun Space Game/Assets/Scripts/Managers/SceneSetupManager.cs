using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Managers
{
    public class SceneSetupManager : Singleton<SceneSetupManager>
    {
        GameObject sceneManager = null;

        private void Awake()
        {
            sceneManager = GameObject.Find("SceneManager(Clone)");

            if(sceneManager == null)
            {
                GameObject newSceneManager = Resources.Load("Prefabs/Managers/SceneManager") as GameObject;
                Instantiate(newSceneManager);
            }
        }
    }
}

