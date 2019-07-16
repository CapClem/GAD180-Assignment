using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; // this means other scripts can refer to this class without having to look for it.
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject zombiePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
