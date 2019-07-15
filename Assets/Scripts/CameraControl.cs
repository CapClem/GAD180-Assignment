using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target1;
    public Transform target2;

    public enum CameraStyle
    {
        fixedPointAngleFollow,
        fixedAngleMoveFollow

    };
    public CameraStyle cameraStyle;



    private Vector3 targetMidPoint;
    private Vector3 initialTargetMidPoint;
    private Vector3 initialCamPosition;

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

        targetMidPoint = (target1.position + (target2.position - target1.position) / 2);


        if (cameraStyle == CameraStyle.fixedPointAngleFollow)
        {
            transform.LookAt(targetMidPoint);

        }
        else if (cameraStyle == CameraStyle.fixedAngleMoveFollow)
        {
            transform.position = initialCamPosition + (targetMidPoint - initialTargetMidPoint);
        }

        transform.position = Vector3.MoveTowards(transform.position,targetMidPoint,-(Vector3.Distance(target1.position,target2.position))/3);

        /* To do----- 
        Zoom to keep players in view.
        this will need to zoom based on the distance between each player,
        relative to the distance between the player Mid point and the camera.

        Desireable.
        Maybe also use the script to put the camera into a good starting position.

        Out of scope.
        maybe make a roaming camera that moves around and rotates to both keep both players in view 
        and avoid obstacles between the camera and players.
         */

    }
}
