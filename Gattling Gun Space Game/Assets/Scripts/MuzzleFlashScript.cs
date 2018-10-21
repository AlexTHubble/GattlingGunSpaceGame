using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashScript : MonoBehaviour {

    [SerializeField]
    int updatesUntillDestroy = 3;
    int updateCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(updateCount < updatesUntillDestroy)
        {
            updateCount++;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
}
