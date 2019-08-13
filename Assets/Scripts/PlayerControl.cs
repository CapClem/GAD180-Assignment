using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
     
        if (Camera.main.gameObject.GetComponent<CameraControl>() != null)
        {
            if (playerNumber == 1)
            {
                Camera.main.gameObject.GetComponent<CameraControl>().target1 = transform;
            }
            else
            {
                Camera.main.gameObject.GetComponent<CameraControl>().target2 = transform;
            }
        }
        if (charMove != null)
        { 
            charMove.enabled = true;
        }

        if (FindObjectOfType<ZombieSpawner>() && FindObjectOfType<UIManager>())
        {
            zSpawner = FindObjectOfType<ZombieSpawner>();
            uiMan = FindObjectOfType<UIManager>();
            if (playerNumber == 1)
            {
                zSpawner.PlayerOne = gameObject;
                uiMan.playerLObject = gameObject;
            }
            else
            {
                zSpawner.PlayerTwo = gameObject;
                uiMan.playerRObject = gameObject;
            }
        }
    }

    ZombieSpawner zSpawner;
    UIManager uiMan;
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
        jumpButton,swapWeaponsButton,reloadButton,pickupButton,alternateFireButton,fireButton
        ,shareButton,optionButton,pauseButton;
    public int controllerNumber = 1;
    public int playerNumber = 1;

    // game components
    CharacterMovement charMove;


    // Start is called before the first frame update
    void Start()
    {
        //this takes a controller number and a ControlScheme
        setUpInputs(controllerNumber, myControlScheme);

        charMove = gameObject.GetComponent<CharacterMovement>();
        charMove.enabled = false;

        if (Camera.main.gameObject.GetComponent<CameraControl>() != null)
        {
            if (playerNumber == 1)
            {
                Camera.main.gameObject.GetComponent<CameraControl>().target1 = transform;
            }
            else
            {
                Camera.main.gameObject.GetComponent<CameraControl>().target2 = transform;
            }
            charMove.enabled = true;
        }

    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
                swapWeaponsButton = "J" + Number + "OButton";
                pickupButton = "J" + Number + "TriangleButton";
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
                pickupButton = "EButton";
                swapWeaponsButton = "QButton";
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
        
    }
}
