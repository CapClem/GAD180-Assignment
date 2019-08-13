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
    private GameObject player;
    private Stats pStats;
    private PlayerControl pCntrl;
    private bool bounceDir = false;
    private float curBouncePercent = 0, maxBounceHeight = 1.3f, minBounceHeight = 1,ramp=0;
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
        transform.Rotate(new Vector3(0, 20* Time.deltaTime, 0),Space.World);
        if (bounceDir)
        {
            if (curBouncePercent <= 0.5f)
            {
                ramp += 30 * Time.deltaTime;
            }
            else
            {
                ramp -= 30 * Time.deltaTime;
            }


            
            curBouncePercent += 0.2f * Time.deltaTime *ramp;
            
            if (curBouncePercent >= 1.0f)
            {
                bounceDir = false;
                ramp = 0.01f;
            }
        }
        else
        {
            if (curBouncePercent >= 0.5f)
            {
                ramp += 30 * Time.deltaTime;
            }
            else
            {
                ramp -= 30 * Time.deltaTime;
            }

            curBouncePercent -= 0.2f * Time.deltaTime * ramp;
            if (curBouncePercent <= 0)
            {
                bounceDir = true;
                ramp = 0.01f;
            }
        }
        transform.position =  new Vector3 (transform.position.x,Mathf.Lerp(minBounceHeight,maxBounceHeight,curBouncePercent),transform.position.z);


        if (player != null && Input.GetButtonDown(pCntrl.pickupButton))
        {
            weaponPickUp();
        }
    }
    void weaponPickUp()
    {
        // Dropping Previous weapon of that type if we have one
        if (pStats.meleWeaponSlot != null && weaponPrefab.GetComponent<MeleWeapon>())
        {
            GameObject newDrop = Instantiate(pStats.meleWeaponSlot.GetComponent<MeleWeapon>().pickUp, transform.position, transform.rotation);
            
            
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
                Destroy(pStats.meleWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.equippedWeaponPos);
                weap.transform.position = pStats.equippedWeaponPos.position;
                MeleWeapon meleWeaponScript = weap.GetComponent<MeleWeapon>();
                pStats.meleWeaponSlot = weap;
                pStats.selectedWeapon = pStats.meleWeaponSlot;
                meleWeaponScript.pickedUp(player,this);

            }
            else
            {
                Destroy(pStats.meleWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.unequippedMeleWeaponPos);
                weap.transform.position = pStats.unequippedMeleWeaponPos.position;
                MeleWeapon meleWeaponScript = weap.GetComponent<MeleWeapon>();
                meleWeaponScript.pickedUp(player,this);
                pStats.meleWeaponSlot = weap;
            }
            
        }
        else
        {
            
            if (pStats.selectedWeapon.GetComponent<RangedWeapon>())
            {
                Destroy(pStats.rangedWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.equippedWeaponPos);
                weap.transform.position = pStats.equippedWeaponPos.position;
                RangedWeapon rangedWeaponScript = weap.GetComponent<RangedWeapon>();
                rangedWeaponScript.pickedUp(player,this);
                rangedWeaponScript.ammo = Value;
                pStats.rangedWeaponSlot = weap;
                pStats.selectedWeapon = pStats.rangedWeaponSlot;
            }
            else
            {
                
                Destroy(pStats.rangedWeaponSlot);
                GameObject weap = Instantiate(weaponPrefab, pStats.unequippedRangedWeaponPos);
                weap.transform.position = pStats.unequippedRangedWeaponPos.position;
                RangedWeapon rangedWeaponScript = weap.GetComponent<RangedWeapon>(); 
                rangedWeaponScript.pickedUp(player,this);
                rangedWeaponScript.ammo = Value;
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
                    if (pStats.increaseAmmo(Value))
                    {
                        Destroy(gameObject);
                    }


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
