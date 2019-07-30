using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{ health, ammo, weapon }

public class PickUpScript : MonoBehaviour
{
    public PickupType myType;
    public int Value; // to use for ammo or health i guess.
    public int cooldownValue = 3;
    public GameObject weaponPrefab;
    public GameObject pickUpModel;
    private Weapon weaponScript;
    private void Start()
    {
        switch (myType)
        {
            case PickupType.health:
                // someone else might wanna do this, Not blaide.

                break;

            case PickupType.ammo:
                // someone else might wanna do this. Not blaide... 

                break;

            case PickupType.weapon:
                weaponScript = weaponPrefab.GetComponent<Weapon>();
                pickUpModel = weaponScript.Model;
                break;

            default:
                Debug.Log("Something has gone terribly wrong.");
                break;
        }

        if (pickUpModel != null)
        {
            GameObject CurrentModel = Instantiate(pickUpModel, transform);
        }
    }
    private void Update()
    {
        // maybe animate the pickup a little here.
        // probably just move current model up and down with a lerp based on time.delta time.

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject player = col.gameObject;
            player.GetComponent<Stats>();
            
            switch (myType)
            {
                case PickupType.health:
                    // someone else might wanna do this, Not blaide.
                    
                    break;
                case PickupType.ammo:
                    // someone else might wanna do this. Not blaide... 

                    break;
                case PickupType.weapon:

                    weaponScript.pickedUp(player);
                    break;

                default:
                    Debug.Log("Something has gone terribly wrong.");
                    break;
            }
            //bye
            Destroy(gameObject);
        }
    }
}
