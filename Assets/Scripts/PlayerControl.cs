using System.Collections;
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
    private string leftVertical, leftHorizontal,rightVertical,rightHorizontal,
        dPadVertical,dpadHorizontal,
        jumpButton,oButton,reloadButton,PickupButton,alternateFireButton,fireButton
        ,shareButton,optionButton,pauseButton;
    public int controllerNumber = 1;

    // game components
    Rigidbody rb;
    CharacterController charCtrl;

    //RotatedHeadings
    private Vector3 forward, right;

    //Movement
    public float speed = 5;
    public float jumpSpeed = 5;
    public float stickFilter = 0;
    private Vector3 moveDir;
    //Gravity
    public float gravity = 10.0f;

    //bool lastInput = false;

    // Start is called before the first frame update
    void Start()
    {
        //this takes a controller number and a ControlScheme
        setUpInputs(controllerNumber, myControlScheme);

        //GetComponents
        rb = this.GetComponent<Rigidbody>();
        charCtrl = this.GetComponent<CharacterController>();

        //headings
        // this is where we rotate the players directions to isometric
        forward = Quaternion.Euler(0, 45, 0) * this.transform.forward;
        right = Quaternion.Euler(0, 90, 0) * forward;

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
        // checks for jump button, and wether or not the player is on the ground, If so, It adds to the move direction.
        if (Input.GetButton(jumpButton) && charCtrl.isGrounded)
        {
            moveDir.y = jumpSpeed;
        }

        if (Mathf.Abs(Input.GetAxis(leftVertical)) > stickFilter || Mathf.Abs(Input.GetAxis(leftHorizontal)) > stickFilter)
        {
            Vector3 verticalMovement = forward * Input.GetAxis(leftVertical);
            Vector3 horizontalMovement = right * Input.GetAxis(leftHorizontal);
            //moveDir=((verticalMovement + horizontalMovement) * speed);
            charCtrl.Move(((verticalMovement + horizontalMovement) * speed) * Time.deltaTime);
            // Debug.Log(" Character moved" + (verticalMovement + horizontalMovement));
        }
        moveDir.y -= gravity * Time.deltaTime;
        charCtrl.Move(moveDir * Time.deltaTime);
    }
}
