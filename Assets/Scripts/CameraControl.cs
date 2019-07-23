using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target1;
    public Transform target2;

    private Vector3 targetMidPoint;
    private Vector3 initialTargetMidPoint;
    private Vector3 initialCamPosition;
    private Vector3 cameraMovePos;
    public float maxPlayerDistance = 80;
    public float playerDistance;
    public float cameraSmoothing = 3;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        // These are used to move the cam relative to the mid point while keeping what ever offset there was to start with.
        initialTargetMidPoint = (target1.position + (target2.position - target1.position) / 2);
        initialCamPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //The following formula is used to find the mid point between two points  A + (B - A) / 2;
        playerDistance = Vector3.Distance(target1.position, target2.position);

        targetMidPoint = (target1.position + (target2.position - target1.position) / 2);

        transform.position = initialCamPosition + (targetMidPoint - initialTargetMidPoint);

        cameraMovePos = Vector3.Lerp(transform.position,targetMidPoint +(transform.position*0.2f), map(playerDistance,0,maxPlayerDistance,1,0));

        if (Vector3.Distance(cameraMovePos, transform.position) > 0.05)
        {
            t += Time.deltaTime * cameraSmoothing;

            transform.position = Vector3.Lerp(transform.position, cameraMovePos, t);
        }
        else
        {
            t = 0;
        }

        /* To do----- 
        Zoom to keep players in view.
        relative to the distance between the player Mid point and the camera.

        Desireable.
        Maybe also use the script to put the camera into a good starting position.

        Out of scope.
        maybe make a roaming camera that moves around and rotates to both keep both players in view 
        and avoid obstacles between the camera and players.
         */

    }

    // using this to convert the distance between the players to a float from 0 to 1.
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
