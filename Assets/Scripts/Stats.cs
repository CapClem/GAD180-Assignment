using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float healthPercent;

    public float health;
    public bool incapacitated = false;
    public float speed = 5;
    public float damageReduction = 0.5f;
    public float reviveSpeed = 2;
    public int shotgunAmmo;
    public int pistolAmmo;
    public int rifleAmmo;
    public GameObject meleWeaponSlot;
    public GameObject rangedWeaponSlot;
    public Transform equippedWeaponPos;
    public Transform unequippedMeleWeaponPos;
    public Transform unequippedRangedWeaponPos;
    public GameObject selectedWeapon;
    public GameObject playerModel;
    public int Kills;
    public Stats lastHitter;

    //public WeaponStats current ;
  
    public MeleWeaponType specialty;
    public enum CharacterType {player,zombie};
    public CharacterType characterType;

    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = meleWeaponSlot;
    }
    void SetPlayerModel()
    {

    }
    // Update is called once per frame
    void Update()
    {
        healthPercent = health / maxHealth;
        if (health <= 0)
        {
            incapacitated = true;
        }
    }

    public void heal(float ammount)
    {
        // alex did this bit.
        if (ammount + health > maxHealth)
        {
            health = 100;
        }
        else
        {
            health += ammount;
        }
    }
    public void Revive()
    {
        incapacitated = false;
        health = 25;
    }
    public void Knockdown()
    {
        incapacitated = true;
    }
    public void SwitchWeapons()
    {
        if ( selectedWeapon == meleWeaponSlot && rangedWeaponSlot != null)
        {
            selectedWeapon.transform.SetParent(unequippedMeleWeaponPos,false);
            selectedWeapon = rangedWeaponSlot;
            selectedWeapon.transform.SetParent(equippedWeaponPos,false);
        }
        else if (selectedWeapon == rangedWeaponSlot && meleWeaponSlot != null)
        {
            selectedWeapon.transform.SetParent(unequippedRangedWeaponPos,false);
            selectedWeapon = meleWeaponSlot;
            selectedWeapon.transform.SetParent(equippedWeaponPos,false);

        }
        else
        {
            Debug.Log("No other weapon!");
        }
    }
    public void TakeDamage(float damage, Stats eStats)
    { 
        if (health > 0) 
        { 
            health -= damage * damageReduction;
        }
        if (health < 0)
        {
            health = 0;
        }
        lastHitter = eStats;
    }

    public void increaseAmmo(int ammount)
    {
        // alex did this bit.
        switch (rangedWeaponSlot.GetComponent<RangedWeapon>().ammoType)
        {
            case AmmoType.pistol:
            pistolAmmo += ammount *15;
                break;
            case AmmoType.shotgun:
                shotgunAmmo += ammount*2;
                break;
            case AmmoType.rifle:
                rifleAmmo += ammount *50;
                break;
            default:

                break;
        }
   }

}
