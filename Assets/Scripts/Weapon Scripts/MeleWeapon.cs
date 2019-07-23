using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleWeaponType { Bladed, Blunt, Heavy, None};

public class MeleWeapon : Weapon
{
    public float range;
    public List<MeleWeaponType> types;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Attack()
    {
        if (types.Contains(playerStats.specialty))
        {

        }

    }
}
