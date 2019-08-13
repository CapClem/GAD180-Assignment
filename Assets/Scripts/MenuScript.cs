using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //Menu States
    public enum MenuStates { Main, Controls, Level, Character };
    public MenuStates currentState;

    //Menu panel objects
    public GameObject mainMenu, controls, level, character;

    //
    public Image pOneCharacterImage, pTwoCharacterImage;
    public Sprite characterOne, characterTwo, characterThree;
    private string LevelSelected;

    public GameObject playerOne, playerTwo;
    private int pOneModel = 1, pTwoModel = 1;


    //When script first starts - Starts on the Main Menu
    void Awake()
    {
        currentState = MenuStates.Main;
    }

    void Update()
    {
        //Updating when menu changes
        switch (currentState)
        {
            //Case for when the Main Menu is Active
            case MenuStates.Main:
                level.SetActive(false);
                mainMenu.SetActive(true);
                controls.SetActive(false);
                character.SetActive(false);
                break;

            //Case for when the Controls Menu is Active
            case MenuStates.Controls:
                level.SetActive(false);
                controls.SetActive(true);
                mainMenu.SetActive(false);
                character.SetActive(false);
                break;

            //Case for when the Character Menu is Active
            case MenuStates.Level:
                level.SetActive(true);
                mainMenu.SetActive(false);
                controls.SetActive(false);
                character.SetActive(false);
                break;

            case MenuStates.Character:
                character.SetActive(true);
                level.SetActive(false);
                mainMenu.SetActive(false);
                controls.SetActive(false);
                break;
        }
    }

    //When Start Game Button is pressed
    public void OnStartGame()
    {
        //Changes the menu state to the Level Select Menu
        currentState = MenuStates.Level;
    }

    //When Controls Button is pressed
    public void OnControls()
    {
        //Changes the menu state to the Controls Menu
        currentState = MenuStates.Controls;
    }

    //When Main Menu Button is pressed
    public void OnMainMenu()
    {
        //Changes the menu state to the Main Menu
        currentState = MenuStates.Main;
    }

    //When Exit Button is pressed
    public void OnExit()
    {
        Debug.Log("You pressed Exit");
    }

    //On Level Select, will load the Level Scene 
    public void LoadLevel(string levelName)
    {
        LevelSelected = levelName;
        currentState = MenuStates.Character;

    }
    public void OnPlay()
    {
        playerOne.GetComponent<Stats>().setPlayerModel(pOneModel);
        playerTwo.GetComponent<Stats>().setPlayerModel(pTwoModel);
        SceneManager.LoadScene(LevelSelected);

    }
    public void onCycle(int i)
    {
        if(i == 1)
        {
            switch (pOneModel)
            {
                case 1:
                case 2:
                    pOneModel++;
                    break;

                case 3:
                    pOneModel = 1;
                    break;

                default:
                    pOneModel = 1;
                    Debug.Log("some How the player model selection cycled further than 3 or lower than 1");
                    break;
            }
        }
        else if(i == 2)
        {
            switch (pTwoModel)
            {
                case 1:
                case 2:
                    pTwoModel++;
                    break;

                case 3:
                    pTwoModel = 1;
                    break;

                default:
                    pTwoModel = 1;
                    Debug.Log("some How the player model selection cycled further than 3 or lower than 1");
                    break;
            }

        }
        updateCharacterImages();
    }
    public void updateCharacterImages()
    {
        switch (pOneModel)
        {
            case 1:
                pOneCharacterImage.sprite = characterOne;
                break;
            case 2:
                pOneCharacterImage.sprite = characterTwo;
                break;
            case 3:
                pOneCharacterImage.sprite = characterThree;
                break;

        }
        
        switch (pTwoModel)
        {
            case 1:
                pTwoCharacterImage.sprite = characterOne;
                break;
            case 2:
                pTwoCharacterImage.sprite = characterTwo;
                break;
            case 3:
                pTwoCharacterImage.sprite = characterThree;
                break;
        }

    }
}
