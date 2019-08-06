using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image pLHealthBar;
    public Image pRHealthBar;

    public GameObject playerLObject;
    public GameObject playerRObject;

    // Start is called before the first frame update
    void Start()
    {
        //playerLObject = GameObject.FindGameObjectWithTag("Player One");
        //playerRObject = GameObject.FindGameObjectWithTag("Player Two");
    }

    // Update is called once per frame
    void Update()
    {
        GetHealth();
    }

    void GetHealth()
    {
        pLHealthBar.fillAmount = playerLObject.GetComponent<Stats>().healthPercent;
        pRHealthBar.fillAmount = playerRObject.GetComponent<Stats>().healthPercent;
    }
}
