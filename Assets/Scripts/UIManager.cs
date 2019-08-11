using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image pLHealthBar;
    public Image pRHealthBar;
    public Image pLRanged;
    public Image pRRanged;
    public Image pLMele;
    public Image pRMele;
    public GameObject playerLObject;
    public GameObject playerRObject;
    public Text pLAmmo;
    public Text pRAmmo;
    public Text pLKills;
    public Text pRKills;
    public Text totalKills;
    // Start is called before the first frame update
    void Start()
    {
        //playerLObject = GameObject.FindGameObjectWithTag("Player One");
        //playerRObject = GameObject.FindGameObjectWithTag("Player Two");
    }

    // Update is called once per frame
    void Update()
    {
        GetHealth();
        GetAmmo();
        GetWeaponIcons();
        GetScore();
    }

    void GetHealth()
    {
        if (playerLObject)
        {
            pLHealthBar.fillAmount = playerLObject.GetComponent<Stats>().healthPercent;
        }
        if (playerRObject)
        {
            pRHealthBar.fillAmount = playerRObject.GetComponent<Stats>().healthPercent;
        }
    }
    void GetWeaponIcons()
    {
        if (playerLObject != null)
        {
            if (playerLObject.GetComponent<Stats>().rangedWeaponSlot !=null)
            {
                pLRanged.sprite = playerLObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().Icon;
                pLRanged.color = new Color(255,255,255,255);
            }
            if (playerLObject.GetComponent<Stats>().meleWeaponSlot)
            {
                pLMele.sprite = playerLObject.GetComponent<Stats>().meleWeaponSlot.GetComponent<MeleWeapon>().Icon;
                pLMele.color = new Color(255, 255, 255, 255);
            }
        }
        if (playerRObject != null)
        {
            if (playerRObject.GetComponent<Stats>().rangedWeaponSlot != null)
            {
                pRRanged.sprite = playerRObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().Icon;
                pRRanged.color = new Color(255, 255, 255, 255);
            }
            if (playerRObject.GetComponent<Stats>().meleWeaponSlot)
            {
                pRMele.sprite = playerRObject.GetComponent<Stats>().meleWeaponSlot.GetComponent<MeleWeapon>().Icon;
                pRMele.color = new Color(255, 255, 255, 255);
            }
        }
    }
    void GetAmmo()
    {
        if (playerLObject != null)
        {
            if (playerLObject.GetComponent<Stats>().rangedWeaponSlot != null)
            {
                if (playerLObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammoType == AmmoType.pistol)
                {
                    pLAmmo.text = playerLObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammo.ToString()+"/" + playerLObject.GetComponent<Stats>().pistolAmmo.ToString();
                }
                if (playerLObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammoType == AmmoType.shotgun)
                {
                    pLAmmo.text = playerLObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammo.ToString() + "/" + playerLObject.GetComponent<Stats>().shotgunAmmo.ToString();
                }

            }
        }
        if (playerRObject != null)
        {
            if (playerRObject.GetComponent<Stats>().rangedWeaponSlot != null)
            {
                if (playerRObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammoType == AmmoType.pistol)
                {
                    pRAmmo.text = playerRObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammo.ToString() + "/" + playerRObject.GetComponent<Stats>().pistolAmmo.ToString();
                }
                if (playerRObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammoType == AmmoType.shotgun)
                {
                    pRAmmo.text = playerRObject.GetComponent<Stats>().rangedWeaponSlot.GetComponent<RangedWeapon>().ammo.ToString() + "/" + playerRObject.GetComponent<Stats>().shotgunAmmo.ToString();
                }
            }
        }
    }
    void GetScore()
    {
        if (playerLObject)
        {
            pLKills.text = playerLObject.GetComponent<Stats>().Kills.ToString();
        }
        if (playerRObject)
        {
             pRKills.text = playerRObject.GetComponent<Stats>().Kills.ToString();
        }
        totalKills.text = (playerLObject.GetComponent<Stats>().Kills + playerRObject.GetComponent<Stats>().Kills).ToString();
    }
}
