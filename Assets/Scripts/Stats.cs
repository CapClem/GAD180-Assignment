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
    public int Ammo = 50;
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
        healthPercent = health / maxHealth;
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
    public void SwitchWeapons()
    {
        if ( selectedWeapon == meleeWeaponSlot && rangedWeaponSlot != null)
        {
            selectedWeapon.transform.SetParent(unequippedWeaponPos,false);
            selectedWeapon = rangedWeaponSlot;
            selectedWeapon.transform.SetParent(equippedWeaponPos,false);
        }
        else if (selectedWeapon == rangedWeaponSlot && meleeWeaponSlot != null)
        {
            selectedWeapon.transform.SetParent(unequippedWeaponPos,false);
            selectedWeapon = meleeWeaponSlot;
            selectedWeapon.transform.SetParent(equippedWeaponPos,false);

        }
        else
        {
            Debug.Log("No other weapon!");
        }
    }
    public void TakeDamage(float damage)
    { 
        if (health > 0) 
        { 
            health -= damage * damageReduction;
        }
    }

   public void increaseAmmo(int ammount)
    {
        // alex did this bit.
        Ammo += ammount;
    }

   public void decreaseAmmo(int ammount)
    {
        Ammo -= ammount;
    }

}
