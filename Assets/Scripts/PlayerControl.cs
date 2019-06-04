using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //controller stuff
    private string leftVertical, leftHorizontal,rightVertical,rightHorizontal,
        dPadVertical,dpadHorizontal,
        leftTriggerAxis,rightTriggerAxis,leftJoyButton,rightJoyButton,
        xButton,oButton,squareButton,triangleButton,leftTriggerButton,rightTriggerButton
        ,shareButton,optionButton,psButton;
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


    bool lastInput = false;

    // Start is called before the first frame update
    void Start()
    {
        setUpInputs(controllerNumber, "DS4");

        //GetComponents
        rb = this.GetComponent<Rigidbody>();
        charCtrl = this.GetComponent<CharacterController>();

        //headings
        forward = Quaternion.Euler(0, 45, 0) * this.transform.forward;
        right = Quaternion.Euler(0, 90, 0) * forward;

    }

    void setUpInputs(int Number, string CtrlrType)
    {
        if (CtrlrType == "DS4")
        {
            //left thumbstick
            leftVertical = "J" + Number + "LVertical";
            leftHorizontal = "J" + Number + "LHorizontal";

            //right thumbstick
            rightVertical = "J" + Number + "RVertical";
            rightHorizontal = "J" + Number + "RHorizontal";

            //left trigger
            leftTriggerAxis = "J" + Number + "LTriggerAxis";
            leftTriggerButton = "J" + Number + "LTriggerButton";

            //right trigger
            rightTriggerAxis = "J" + Number + "RTriggerButton";
            rightTriggerButton = "J" + Number + "RTriggerButton";

            //dpad
            dPadVertical = "J" + Number + "DPadVertical";
            dpadHorizontal = "J" + Number + "DPadHorizontal";

            //Buttons
            leftTriggerButton = "J" + Number + "LTriggerButton";
            rightTriggerButton = "J" + Number + "RTriggerButton";
            xButton = "J" + Number + "XButton";
            oButton = "J" + Number + "OButton";
            triangleButton = "J" + Number + "TriangleButton";
            squareButton = "J" + Number + "SquareButton";
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton(triangleButton) && charCtrl.isGrounded)
        {
            moveDir.y = jumpSpeed;
        }



        if (Mathf.Abs(Input.GetAxis(leftVertical)) > stickFilter || Mathf.Abs(Input.GetAxis(leftHorizontal)) > stickFilter)
        {
            Vector3 verticalMovement = forward * Input.GetAxis(leftVertical);
            Vector3 horizontalMovement = right * Input.GetAxis(leftHorizontal);
            //moveDir=((verticalMovement + horizontalMovement) * speed);
            charCtrl.Move(((verticalMovement + horizontalMovement) * speed) * Time.deltaTime);
            Debug.Log(" Character moved" + (verticalMovement + horizontalMovement));
        }
        moveDir.y -= gravity * Time.deltaTime;
        charCtrl.Move(moveDir * Time.deltaTime);
    }
}
