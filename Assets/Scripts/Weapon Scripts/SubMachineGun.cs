using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMachineGun : RangedWeapon
{
    void Start()
    {
        ammoType = AmmoType.SMG;
    }
    override public void Attack()
    {
        if (timeUntilAttack <= 0 && ammo > 0)
        {
            RaycastHit hit;
            GameObject hitLine = Instantiate(lineRendObj);
            LineRenderer lineRend = hitLine.GetComponent<LineRenderer>();
            lineRend.SetPosition(0, muzzle.position);
            Vector3 lineEnd;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, range))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                lineEnd = hit.point;
                Debug.Log("Hit");
                if (hit.collider.gameObject.GetComponent<Stats>())
                {
                    if (hit.collider.gameObject.GetComponent<Stats>().characterType == Stats.CharacterType.zombie)
                    {
                        hit.collider.gameObject.GetComponent<Stats>().TakeDamage(damage, pStats);
                    }
                }

            }
            else
            {
                lineEnd = transform.position + transform.TransformDirection(Vector3.left) * range;
            }
            lineRend.SetPosition(1, lineEnd);
            Destroy(hitLine, 0.05f);
            timeUntilAttack = attackDelay;
            ammo -= 1;
        }
        else if (ammo <= 0)
        {
            Reload();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (timeUntilAttack > 0)
        {
            timeUntilAttack -= Time.deltaTime;
        }
    }
}
