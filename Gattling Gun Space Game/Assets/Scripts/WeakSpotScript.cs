using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpotScript : MonoBehaviour {

    GameObject playerConnectedTo = null;
    [SerializeField]
    int bulletDamage = 1;

	// Use this for initialization
	void Start ()
    {
        if(gameObject.tag == "P1")
        {
            playerConnectedTo = GameObject.Find("Player");
        }
        if (gameObject.tag == "P2")
        {
            playerConnectedTo = GameObject.Find("Player 2");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.tag == "P1")
        {
            if (collision.gameObject.tag == "P1Bullet")
            {
                Managers.PlayerManager.Instance.reducePl1Hp(bulletDamage, true);
                Debug.Log("P1 hit P1");
            }
            else if (collision.gameObject.tag == "P2Bullet")
            {
                Managers.PlayerManager.Instance.reducePl1Hp(bulletDamage, false);
                Debug.Log("P2 hit P1");
            }
        }
        if (gameObject.tag == "P2")
        {
            if (collision.gameObject.tag == "P2Bullet")
            {
                Managers.PlayerManager.Instance.reducePl2Hp(bulletDamage, true);
                Debug.Log("P2 hit P2");
            }
            else if (collision.gameObject.tag == "P1Bullet")
            {
                Managers.PlayerManager.Instance.reducePl2Hp(bulletDamage, false);
                Debug.Log("P1 hit P2");
            }
        }

    }
}
