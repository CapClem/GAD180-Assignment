using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleCombatBase : MonoBehaviour
{
    private PlayerControl pC;
    private Stats PlayerStats;
    private Stats enemyStats;

    public GameObject hitObj;
    public Vector3 hitColliderOrigin;
    public Vector3 hitDirection;
    public float hitColliderRadius;
    public float maxHitDistance;
    public LayerMask hitColliderMask;
    public float currentHitDistance;

    public bool meleAllowed;
    private float damage;
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
            attack();
        }

        hitColliderOrigin = transform.position;
        hitDirection = pC.face.transform.forward;
    }
    void attack()
    {

        RaycastHit hit;
        if (Physics.SphereCast(hitColliderOrigin, hitColliderRadius, hitDirection, out hit, maxHitDistance, hitColliderMask, QueryTriggerInteraction.UseGlobal))
        {
            hitObj = hit.collider.gameObject;
            currentHitDistance = hit.distance;
            if (hitObj.GetComponent<Stats>())
            {
            enemyStats = hitObj.GetComponent<Stats>();

            if (enemyStats.characterType == Stats.CharacterType.zombie)
            {
                 damage = 10;
                    enemyStats.TakeDamage(damage);
                    hitObj.GetComponent<ZombieControl>().KnockBack(damage,hitDirection);
                Debug.Log( hitObj.name +" got hit for " + damage + " damage.");
            }


           }

        }
        else
        {
            currentHitDistance = maxHitDistance;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(hitColliderOrigin, hitColliderOrigin + hitDirection * currentHitDistance);
        Gizmos.DrawWireSphere(hitColliderOrigin + hitDirection * currentHitDistance, hitColliderRadius);
    }
}
