using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    // game components
    Rigidbody rb;
    CharacterController charCtrl;
    Stats pStats;
    PlayerControl pCtrl;

    public GameObject lookPos;
    public GameObject face;
    public GameObject otherPlayer;
    private Vector3 startPos;

    //RotatedHeadings
    private Vector3 forward, right;

    //Movement
    public float jumpSpeed = 5;
    public float stickFilter = 0;
    private Vector3 moveDir;
   // private float maxDistance = 1;


    //Gravity
    public float gravity = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        forward = Quaternion.Euler(0, 45, 0) * this.transform.forward;
        right = Quaternion.Euler(0, 90, 0) * forward;
        startPos = transform.position;
        pCtrl = gameObject.GetComponent<PlayerControl>();
        charCtrl = gameObject.GetComponent<CharacterController>();
        pStats = gameObject.GetComponent<Stats>();
        rb = gameObject.gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // might move this movement stuff to another script, and enable it when i get to the correct scenes, so that i don't have to keep checking here.
        // will probably call it characterMovement

        if (transform.position.y <= -1)
        {
            transform.position = startPos;
        }
        else
        {
            if (!pStats.incapacitated) {
                // checks for jump button, and wether or not the player is on the ground, If so, It adds to the move direction.
                if (Input.GetButton(pCtrl.jumpButton) && charCtrl.isGrounded)
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
                if (Mathf.Abs(Input.GetAxis(pCtrl.leftVertical)) > stickFilter || Mathf.Abs(Input.GetAxis(pCtrl.leftHorizontal)) > stickFilter)
                {
                    Vector3 verticalMovement = forward * Input.GetAxis(pCtrl.leftVertical);
                    Vector3 horizontalMovement = right * Input.GetAxis(pCtrl.leftHorizontal);

                    if (Vector3.Distance(transform.position + verticalMovement + horizontalMovement, otherPlayer.transform.position) < 100)
                    {
                        charCtrl.Move(((verticalMovement + horizontalMovement) * pStats.speed) * Time.deltaTime);

                    }
                }
                // Applying our own gravity.
                moveDir.y -= gravity * Time.deltaTime;
                // doing the movement we set up with the rest of everything else.
                charCtrl.Move(moveDir * Time.deltaTime);

                // Rotating after having moved.
                // we check to see if the sticks have been moved.
                if (Mathf.Abs(Input.GetAxis(pCtrl.rightVertical)) > stickFilter || Mathf.Abs(Input.GetAxis(pCtrl.rightHorizontal)) > stickFilter)
                {
                    // we check to see if  the keyboard and mouse is being used, we use a different aiming method for it.
                    if (pCtrl.myControlScheme == PlayerControl.ControlScheme.Keyboard)
                    {
                        // we cast a ray
                        RaycastHit hit;
                        int mask = (1 << 10);
                        //see if it hit anything
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100,mask))
                        {
                            // if so we move the look position to there, but at the same height as the player. so that the player doesn't lean over.
                            lookPos.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        }
                    }
                    // if if its not keyboard and mouse, we just move the the look pos around the player porpotionately to the analog stick.
                    else
                    {
                        lookPos.transform.localPosition = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxis(pCtrl.rightHorizontal), 0, Input.GetAxis(pCtrl.rightVertical)).normalized * 3;
                    }
                    // then after all that we make the player look at it,.. 
                    face.transform.LookAt(lookPos.transform.position); // only rotating the face because the parent object being rotated would also move the lookPos Creating a feedback loop of rotation.
                                                                       // this will later just rotate the player.. instead but for now its this.

                }
            }

        }
    }

}
