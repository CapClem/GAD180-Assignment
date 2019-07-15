using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float Ammo;
    public GameObject WeaponSlot;
    public WeaponStats current ;
    public enum Specialty
    {
        guns,
        bladed,
        blunt
    };
    public Specialty specialty;
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
        health -= damage;
    }

   public void increaseAmmo()
    {

    }

   public void decreaseAmmo()
    {

    }

}
