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
  [HideInInspector]
    public string leftVertical, leftHorizontal,rightVertical,rightHorizontal,
        dPadVertical,dpadHorizontal,
        jumpButton,oButton,reloadButton,PickupButton,alternateFireButton,fireButton
        ,shareButton,optionButton,pauseButton;
    public int controllerNumber = 1;

    // game components
    Rigidbody rb;
    CharacterController charCtrl;
    public GameObject lookPos;
    public GameObject face;

    //RotatedHeadings
    private Vector3 forward, right;

    //Movement
    public float speed = 5;
    public float jumpSpeed = 5;
    public float stickFilter = 0;
    private Vector3 moveDir;

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
        rb = this.GetComponent<Rigidbody>();
        charCtrl = this.GetComponent<CharacterController>();
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
                //moveDir=((verticalMovement + horizontalMovement) * speed);
                charCtrl.Move(((verticalMovement + horizontalMovement) * speed) * Time.deltaTime);
                // Debug.Log(" Character moved" + (verticalMovement + horizontalMovement));
                
            }
            // Applying our own gravity.
            moveDir.y -= gravity * Time.deltaTime;
            // doing the movement we set up with the rest of everything else.
            charCtrl.Move(moveDir * Time.deltaTime);

            // Rotating after having moved.
            if (Mathf.Abs(Input.GetAxis(rightVertical)) > stickFilter || Mathf.Abs(Input.GetAxis(rightHorizontal)) > stickFilter)
            {
                lookPos.transform.localPosition = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxis(rightHorizontal), 0, Input.GetAxis(rightVertical)).normalized *3;
                face.transform.LookAt(lookPos.transform.position); // only rotating the face because the parent object being rotated would also move the lookPos Creating a feedback loop of rotation.
            }
            
        }
    }
}
