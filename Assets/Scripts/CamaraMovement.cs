using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovement : MonoBehaviour
{

    public Transform playerTransform;

    public Vector2 minCameraposition;

    public Vector2 maxCameraposition;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform !=null)
        {

            Vector3 desiredPosition = playerTransform.position + offset;

            float clampX = Mathf.Clamp(desiredPosition.x, minCameraposition.x, maxCameraposition.x);
            float clampY = Mathf.Clamp(desiredPosition.y, minCameraposition.y, maxCameraposition.y);

            Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z);
            transform.position = clampedPosition;

        }
    }
}


    