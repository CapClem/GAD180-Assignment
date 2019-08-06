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
    private Weapon weaponScript;
    private GameObject player;
    private Stats pStats;
    private PlayerControl pCntrl;
    private void Start()
    {
        switch (myType)
        {
            case PickupType.health:
                if (Value <= 0)
                {
                    Value = Random.Range(15, 50);
                }

                break;

            case PickupType.ammo:
                // someone else might wanna do this. Not blaide... 
                if( Value <= 0)
                {
                    Value = Random.Range(15, 75);
                }
                break;

            case PickupType.weapon:
                
                break;

            default:
                Debug.Log("Something has gone terribly wrong.");
                break;
        }
    }
    private void Update()
    {

        if (player != null && Input.GetButtonDown(pCntrl.pickupButton))
        {
            weaponPickUp();
        }
    }
    void weaponPickUp()
    {
         
        if (pStats.meleeWeaponSlot != null)
        {
            Instantiate(pStats.meleeWeaponSlot.GetComponent<Weapon>().pickUp, transform.position, transform.rotation);
            Destroy(pStats.meleeWeaponSlot);
        }

        GameObject weap = Instantiate(weaponPrefab, pStats.equippedWeaponPos);
        weap.transform.position = pStats.equippedWeaponPos.position;
        weaponScript = weap.GetComponent<Weapon>();
        weaponScript.player = player;

        if (weaponPrefab.GetComponent<MeleWeapon>())
        {
            pStats.meleeWeaponSlot = weap;
        }
        else
        {
            pStats.rangedWeaponSlot = weap;
        }

        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            pStats = player.GetComponent<Stats>();
            pCntrl = player.GetComponent<PlayerControl>();
            switch (myType)
            {
                case PickupType.health:
                    // Alex did this bit
                    if (pStats.health < 100)
                    {
                        pStats.heal(Value);
                        Destroy(gameObject);
                    }
                    break;
                case PickupType.ammo:
                    // Alex did this bit
                    pStats.increaseAmmo(Value);

                    Destroy(gameObject);
                    break;
                case PickupType.weapon:
                    
                    break;

                default:
                    Debug.Log("Something has gone terribly wrong.");
                    break;
            }
            //bye
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player = null;
        }
    }
}
