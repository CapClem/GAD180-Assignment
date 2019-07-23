using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public bool incapacitated = false;
    public float speed = 5;
    public float damageReduction = 0.5f;
    public float reviveSpeed = 2;
    public float Ammo = 50;
    public GameObject meleeWeaponSlot;
    public GameObject rangedWeaponSlot;
    //public WeaponStats current ;
  
    public MeleWeaponType specialty;
    public enum CharacterType {player,zombie};
    public CharacterType characterType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void heal()
    {

    }
   public void TakeDamage(float damage)
    {
        health -= damage*damageReduction;
    }

   public void increaseAmmo()
    {

    }

   public void decreaseAmmo()
    {

    }

}
