using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleCombatBase : MonoBehaviour
{
    private PlayerControl pC;
    private Stats PlayerStats;
    private Stats enemyStats;
    private float damage;
    private float distance;
    private float meleCoolDown;
    
    // Start is called before the first frame update
    void Start()
    {
        pC = GetComponent<PlayerControl>();
        PlayerStats = GetComponent<Stats>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton(pC.fireButton))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit) && meleCoolDown <= 0)
            {
                distance = hit.distance;
                //hit.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
                damage = PlayerStats.WeaponSlot.GetComponentInChildren<WeaponStats>().damage;

                enemyStats = hit.collider.gameObject.GetComponent<Stats>();
                enemyStats.TakeDamage(damage);
               // meleCoolDown = 
            }
        }
        
    }
}
