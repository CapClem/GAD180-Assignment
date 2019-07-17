using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    //Menu States
    public enum MenuStates {Main, Controls};
    public MenuStates currentState;

    //Menu panel objects
    public GameObject mainMenu;
    public GameObject controls;
    
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
                mainMenu.SetActive(true);
                controls.SetActive(false);
                break;
            
            //Case for when the Controls Menu is Active
            case MenuStates.Controls:
                controls.SetActive(true);
                mainMenu.SetActive(false);
                break;
        }
    }

    //When Start Game Button is pressed
    public void OnStartGame()
    {
        Debug.Log("You pressed Start Game");
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
}
