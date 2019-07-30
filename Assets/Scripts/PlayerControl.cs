﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //controller stuff
    public enum ControlScheme
    {
        DS4,
        XBOX,
        Keyboard,
    };
    public ControlScheme myControlScheme;
    /// <summary>
    /// Fire,move,aim,reloading,jumping,pickup,drop, Menu nav.
    /// </summary>
  [HideInInspector]
    public string leftVertical, leftHorizontal,rightVertical,rightHorizontal,
        dPadVertical,dpadHorizontal,
        jumpButton,oButton,reloadButton,PickupButton,alternateFireButton,fireButton
        ,shareButton,optionButton,pauseButton;
    public int controllerNumber = 1;

    // game components
    Rigidbody rb;
    CharacterController charCtrl;
    Stats playerStats;
    public GameObject lookPos;
    public GameObject face;
    public GameObject otherPlayer;

    //RotatedHeadings
    private Vector3 forward, right;

    //Movement
    public float jumpSpeed = 5;
    public float stickFilter = 0;
    private Vector3 moveDir;
    private float maxDistance = 1;

    //Gravity
    public float gravity = 10.0f;

    private Vector3 startPos;
    //bool lastInput = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //this takes a controller number and a ControlScheme
        setUpInputs(controllerNumber, myControlScheme);

        //GetComponents
        rb = GetComponent<Rigidbody>();
        charCtrl = GetComponent<CharacterController>();
        playerStats = GetComponent<Stats>();

        //headings
        // this is where we rotate the players directions to isometric
        forward = Quaternion.Euler(0, 45, 0) * this.transform.forward;
        right = Quaternion.Euler(0, 90, 0) * forward;
        startPos = transform.position;
    }

    void setUpInputs(int Number, ControlScheme selectedControlScheme)
    {
        switch (selectedControlScheme)
        {
            case ControlScheme.DS4:
                //left thumbstick
                leftVertical = "J" + Number + "LVertical";
                leftHorizontal = "J" + Number + "LHorizontal";

                //right thumbstick
                rightVertical = "J" + Number + "RVertical";
                rightHorizontal = "J" + Number + "RHorizontal";

                //dpad
                dPadVertical = "J" + Number + "DPadVertical";
                dpadHorizontal = "J" + Number + "DPadHorizontal";

                //Buttons
                alternateFireButton = "J" + Number + "LTriggerButton";
                fireButton = "J" + Number + "RTriggerButton";
                jumpButton = "J" + Number + "XButton";
                //oButton = "J" + Number + "OButton";
                PickupButton = "J" + Number + "TriangleButton";
                reloadButton = "J" + Number + "SquareButton";
                pauseButton = "J" + Number + "optionsButton";
                break;

            case ControlScheme.XBOX:
                // leaving this blank for some else to try out.
                break;

            case ControlScheme.Keyboard:
                //left thumbstick
                leftVertical = "KVertical";
                leftHorizontal = "KHorizontal";

                //right thumbstick
                rightVertical = "MVertical";
                rightHorizontal = "MHorizontal";

                //dpad
                dPadVertical = "KVertical";
                dpadHorizontal = "KHorizontal";

                //Buttons
                alternateFireButton = "LMouse";
                fireButton = "RMouse";
                jumpButton = "SpaceBar";
                PickupButton = "EButton";
                reloadButton = "RButton";
                pauseButton = "Esc";
                break;

            default:
                Debug.Log("Control Scheme was Not Set.");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -1)
        {
            transform.position = startPos;
        }
        else
        {
            // checks for jump button, and wether or not the player is on the ground, If so, It adds to the move direction.
            if (Input.GetButton(jumpButton) && charCtrl.isGrounded)
            {
                // We also change these values to stop the character controller from glitching while jumping against walls.
                charCtrl.slopeLimit = 90;
                charCtrl.stepOffset = 0;

                moveDir.y = jumpSpeed;
            }
            else if (charCtrl.isGrounded)
            {
                // we change them back when back on the ground.
                charCtrl.slopeLimit = 45;
                charCtrl.stepOffset = 0.3f;
            }
            // basic movement.
            if (Mathf.Abs(Input.GetAxis(leftVertical)) > stickFilter || Mathf.Abs(Input.GetAxis(leftHorizontal)) > stickFilter)
            {
                Vector3 verticalMovement = forward * Input.GetAxis(leftVertical);
                Vector3 horizontalMovement = right * Input.GetAxis(leftHorizontal);

                if (Vector3.Distance(transform.position + verticalMovement + horizontalMovement, otherPlayer.transform.position) < 100)
                {
                    charCtrl.Move(((verticalMovement + horizontalMovement) * playerStats.speed) * Time.deltaTime);

                }
            }
            // Applying our own gravity.
            moveDir.y -= gravity * Time.deltaTime;
            // doing the movement we set up with the rest of everything else.
            charCtrl.Move(moveDir * Time.deltaTime);

            // Rotating after having moved.
            // we check to see if the sticks have been moved.
            if (Mathf.Abs(Input.GetAxis(rightVertical)) > stickFilter || Mathf.Abs(Input.GetAxis(rightHorizontal)) > stickFilter)
             {
                // we check to see if  the keyboard and mouse is being used, we use a different aiming method for it.
                if (myControlScheme == ControlScheme.Keyboard)
                {
                    // we cast a ray
                    RaycastHit hit;
                    //see if it hit anything
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    {
                        // if so we move the look position to there, but at the same height as the player. so that the player doesn't lean over.
                        lookPos.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    }
                }
                // if if its not keyboard and mouse, we just move the the look pos around the player porpotionately to the analog stick.
                else
                {
                    lookPos.transform.localPosition = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxis(rightHorizontal), 0, Input.GetAxis(rightVertical)).normalized * 3;
                }
                // then after all that we make the player look at it,.. 
                face.transform.LookAt(lookPos.transform.position); // only rotating the face because the parent object being rotated would also move the lookPos Creating a feedback loop of rotation.
                // this will later just rotate the player.. instead but for now its this.

            }

        }
    }

    // this is knockback, but for the current camera set up, it might not be great,.. it makes the camera move suddenly. maybe i need to smooth out this movement. like with a lerp or something.
    public void KnockBack(float dmg, Vector3 dir)
    {
        charCtrl.Move( dir * dmg *0.1f);
    }
}
