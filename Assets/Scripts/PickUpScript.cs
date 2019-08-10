using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{ health, ammo, weapon }

public class PickUpScript : MonoBehaviour
{
    public PickupType myType;
    public int Value; 
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
                    Value = Random.Range(4, 16);
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
        // Dropping Previous weapon of that type if we have one
        if (pStats.meleeWeaponSlot != null && weaponPrefab.GetComponent<MeleWeapon>())
        {
            GameObject newDrop = Instantiate(pStats.meleeWeaponSlot.GetComponent<MeleWeapon>().pickUp, transform.position, transform.rotation);
            
            
        }
        else if ((pStats.rangedWeaponSlot != null && weaponPrefab.GetComponent<RangedWeapon>()))
        {
            GameObject newDrop = Instantiate(pStats.rangedWeaponSlot.GetComponent<RangedWeapon>().pickUp, transform.position, transform.rotation);
            PickUpScript pScript = newDrop.GetComponent<PickUpScript>();
            pScript.Value = pStats.rangedWeaponSlot.GetComponent<RangedWeapon>().ammo;
        }



        if (weaponPrefab.GetComponent<MeleWeapon>())
        {
            if (pStats.selectedWeapon.GetComponent<MeleWeapon>())
            {
                Destroy(pStats.meleeWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.equippedWeaponPos);
                weap.transform.position = pStats.equippedWeaponPos.position;
                weaponScript = weap.GetComponent<MeleWeapon>();
                weaponScript.pickedUp(player, gameObject);
                pStats.meleeWeaponSlot = weap;
                pStats.selectedWeapon = pStats.meleeWeaponSlot;
               
                
            }
            else
            {
                Destroy(pStats.meleeWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.unequippedWeaponPos);
                weap.transform.position = pStats.unequippedWeaponPos.position;
                weaponScript = weap.GetComponent<MeleWeapon>();
                weaponScript.pickedUp(player, gameObject);
                pStats.meleeWeaponSlot = weap;
            }
            
        }
        else
        {
            if (pStats.selectedWeapon.GetComponent<RangedWeapon>())
            {
                Destroy(pStats.rangedWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.equippedWeaponPos);
                weap.transform.position = pStats.equippedWeaponPos.position;
                weaponScript = weap.GetComponent<RangedWeapon>();
                weaponScript.pickedUp(player,gameObject);
                pStats.rangedWeaponSlot = weap;
                pStats.selectedWeapon = pStats.rangedWeaponSlot;
            }
            else
            {
                
                Destroy(pStats.rangedWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.unequippedWeaponPos);
                weap.transform.position = pStats.unequippedWeaponPos.position;
                weaponScript = weap.GetComponent<RangedWeapon>();
                weaponScript.pickedUp(player, gameObject);
                pStats.rangedWeaponSlot = weap;

            }
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
