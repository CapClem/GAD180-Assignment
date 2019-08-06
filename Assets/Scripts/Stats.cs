using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public float healthPercent;

    public float health;
    public bool incapacitated = false;
    public float speed = 5;
    public float damageReduction = 0.5f;
    public float reviveSpeed = 2;
    public float Ammo = 50;
    public GameObject meleeWeaponSlot;
    public GameObject rangedWeaponSlot;
    public Transform equippedWeaponPos;
    public Transform unequippedWeaponPos;
    public GameObject selectedWeapon;
    //public WeaponStats current ;
  
    public MeleWeaponType specialty;
    public enum CharacterType {player,zombie};
    public CharacterType characterType;

    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = meleeWeaponSlot;
    }

    // Update is called once per frame
    void Update()
    {
        healthPercent = currentHealth / maxHealth;
    }

   public void heal(float ammount)
    {

    }
    public void SwitchWeapons()
    {
        if ( selectedWeapon == meleeWeaponSlot)
        {
            selectedWeapon.transform.SetParent(unequippedWeaponPos, false);
            selectedWeapon = rangedWeaponSlot;
        }
        else
        {
            selectedWeapon.transform.SetParent(unequippedWeaponPos, false);
            selectedWeapon.transform.rotation = unequippedWeaponPos.rotation;
            selectedWeapon = meleeWeaponSlot;
            selectedWeapon.transform.SetParent(equippedWeaponPos, false);
            selectedWeapon.transform.rotation = unequippedWeaponPos.rotation;
        }
    }
   public void TakeDamage(float damage)
    {
        health -= damage*damageReduction;
    }

   public void increaseAmmo(int ammount)
    {

    }

   public void decreaseAmmo(int ammount)
    {

    }

}
