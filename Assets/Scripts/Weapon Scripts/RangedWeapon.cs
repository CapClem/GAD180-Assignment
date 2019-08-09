using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AmmoType {shotgun,pistol,rifle}
public class RangedWeapon : Weapon
{
    public float reloadTime;
    public int ammo;
    public int clipSize;
    public Transform muzzle;
    public GameObject lineRendObj;
    public bool auto;
    [HideInInspector]
    public AmmoType ammoType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Reload()
    {
        if (ammoType == AmmoType.pistol)
        {
            if (pStats.pistolAmmo > clipSize - ammo)
            {
                pStats.pistolAmmo -= (clipSize - ammo);
                ammo = clipSize;
                Debug.Log("just reloaded, pistol ammo left:" + pStats.pistolAmmo);
            }
            else if (pStats.pistolAmmo > 0)
            {
                ammo += pStats.pistolAmmo;
                pStats.pistolAmmo = 0;
                Debug.Log("just reloaded, pistol ammo left:" + pStats.pistolAmmo);
            }
            else
            {
                Debug.Log("No Ammo");
            }
        }
        else if (ammoType == AmmoType.shotgun)
        {
            if (pStats.shotgunAmmo > clipSize - ammo)
            {
                pStats.shotgunAmmo -= (clipSize - ammo);
                ammo = clipSize;
                Debug.Log("just reloaded, shotgun ammo left:" + pStats.shotgunAmmo);
            }
            else if (pStats.shotgunAmmo > 0)
            {
                ammo += pStats.shotgunAmmo;
                pStats.shotgunAmmo = 0;
                Debug.Log("just reloaded, shotgun ammo left:" + pStats.shotgunAmmo);
            }
            else
            {
                Debug.Log("No Ammo");
            }
        }
            
    }
    public virtual void Attack()
    {
       // Debug.Log("Im a ranged Weapon and i just fired.");

    }
    // Update is called once per frame
    void Update()
    {

    }
}
