using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomToPlayerScript : MonoBehaviour
{

    Transform cameraTransform;
    Camera mainCamera;

    bool isMoving;

    Vector3 target;

    [SerializeField]
    float cameraSpeed = 1;
    [SerializeField]
    float targetCameraSize = 5;
    [SerializeField]
    float cameraShrinkSpeed = .25f;

	// Use this for initialization
	void Start ()
    {
        //isMoving = true;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cameraTransform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isMoving)
        {
            moveToTarget();
        }
    }

    public void startMoving(Vector3 pos)
    {
        target = pos;
        isMoving = true;
    }

    private void moveToTarget()
    {
        if(mainCamera.orthographicSize > targetCameraSize)
        {
            mainCamera.orthographicSize -= cameraShrinkSpeed; 
        }

        Vector3 cameraPosition = cameraTransform.position;

        if(cameraPosition.x != target.x || cameraPosition.y != target.y)
        {
            cameraPosition = Vector2.MoveTowards(cameraTransform.position, target, cameraSpeed * Time.deltaTime);
            cameraTransform.position = cameraPosition;
        }
    }
}
