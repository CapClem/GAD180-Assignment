using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AmmoType {shotgun,pistol,rifle,assaultRifle,SMG}
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
        switch (ammoType)
        {
            case AmmoType.pistol:
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
                timeUntilAttack = reloadTime;
                break;

            case AmmoType.SMG:
                if (pStats.smgAmmo > clipSize - ammo)
                {
                    pStats.smgAmmo -= (clipSize - ammo);
                    ammo = clipSize;
                    Debug.Log("just reloaded, smg ammo left:" + pStats.smgAmmo);
                }
                else if (pStats.smgAmmo > 0)
                {
                    ammo += pStats.smgAmmo;
                    pStats.smgAmmo = 0;
                    Debug.Log("just reloaded, smg ammo left:" + pStats.smgAmmo);
                }
                else
                {
                    Debug.Log("No Ammo");
                }
                timeUntilAttack = reloadTime;
                break;

            case AmmoType.shotgun:
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
                timeUntilAttack = reloadTime;
                break;

            case AmmoType.rifle:
            case AmmoType.assaultRifle:
                Debug.Log("Not Set Up Yet");
                break;
        }
   
    }
    public override void pickedUp(GameObject p,PickUpScript pS)
    {
        base.pickedUp(p,pS);
        ammo = pS.Value;
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
