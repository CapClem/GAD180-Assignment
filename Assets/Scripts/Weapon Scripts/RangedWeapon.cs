using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public float reloadTime;
    public float fireDelay;
    public int ammo;
    public int clipSize;
    public Transform Muzzle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Reload()
    {
        if (pStats.Ammo > clipSize - ammo)
        {
            pStats.decreaseAmmo(clipSize - ammo);
            ammo = clipSize;
        }
        else if (pStats.Ammo > 0)
        {
            ammo += pStats.Ammo;
            pStats.decreaseAmmo(pStats.Ammo);
        }
        else
        {
            Debug.Log("No Ammo");
        }
            
    }
    void Attack()
    {
        Debug.Log("Im a ranged Weapon and i just fired.");

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(pCtrl.fireButton))
        {
            Attack();
        }
    }
}
