using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Stats pStats;
    [HideInInspector]
    public PlayerControl pCtrl;
    public float damage;
    public GameObject pickUp;
    public float attackDelay;
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public float timeUntilAttack;
    public float range;
    public Sprite Icon;
    // public float 
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = player.GetComponent<PlayerControl>();
        pStats = player.GetComponent<Stats>();
    }

    public virtual void pickedUp(GameObject p, PickUpScript pS)
    {
        player = p;
        pCtrl = player.GetComponent<PlayerControl>();
        pStats = player.GetComponent<Stats>();
    }
    // Update is called once per frame
    void Update()
    {


    }
}
