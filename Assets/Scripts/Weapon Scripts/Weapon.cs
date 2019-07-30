using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject player;
    public Stats playerStats;
    public PlayerControl playerControl;
    public float damage;
    public float cooldown;
    public GameObject Model;

    // public float 
    // Start is called before the first frame update
    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        playerStats = player.GetComponent<Stats>();
    }
    void Attack()
    {

    }
    public void pickedUp(GameObject p)
    {
        player = p;
        playerControl = player.GetComponent<PlayerControl>();
        playerStats = player.GetComponent<Stats>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(playerControl.fireButton))
        {
            Attack();
        }

    }
}
