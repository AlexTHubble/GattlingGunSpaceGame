using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CameraShake : MonoBehaviour {

    Player player1;
    Player player2;

    Vector3 origionalCameraPosition;
    Camera camera = null;
    Transform cameraTransform;

    [SerializeField]
    float shakeAmmount = 0.7f;

    [SerializeField]
    float shakeDuration = 1f;

    float currentShakeDuration = 0f;

    [SerializeField]
    float decreaseFactor = 1.0f;

    bool startCameraShake = false;

	// Use this for initialization
	void Start ()
    {
        camera = this.gameObject.GetComponent<Camera>();
        cameraTransform = camera.transform;
        player2 = ReInput.players.GetPlayer("Player1");
        player1 = ReInput.players.GetPlayer("Player0");

        origionalCameraPosition = camera.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if(player2.GetButtonDown("ShootGun") || player1.GetButtonDown("ShootGun"))
        {
            currentShakeDuration = shakeDuration;
        }

        cameraShake();

	}

    void cameraShake()
    {
        if(currentShakeDuration > 0)
        {
            cameraTransform.localPosition = origionalCameraPosition + Random.insideUnitSphere * shakeAmmount;

            currentShakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            currentShakeDuration = 0f;
            cameraTransform.localPosition = origionalCameraPosition;
        }
    }

    void stopShaking()
    {

    }

}
