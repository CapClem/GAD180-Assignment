using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    private PlayerControl pCtrl;
    private Stats pStats;
    private Stats enemyStats;
    private CharacterMovement charMove;
    [HideInInspector]
    public Weapon currentWeapon;
    [HideInInspector]
    public GameObject hitObj;
    private Vector3 hitColliderOrigin;
    private Vector3 hitDirection;
    public float hitColliderRadius;
    [HideInInspector]
    public float maxHitDistance;
    public LayerMask hitColliderMask;
    private float currentHitDistance;

    [HideInInspector]
    public bool meleAllowed;
    private float damage;
    private float meleCoolDown;

    private MeleWeapon meleWeap;
    private RangedWeapon rangedWeap;
    
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerControl>();
        charMove = GetComponent<CharacterMovement>();
        pStats = GetComponent<Stats>();
        
    }
    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown(pCtrl.swapWeaponsButton))
       {
         pStats.SwitchWeapons();
       }

        if (Input.GetButton(pCtrl.fireButton))
        {
            if ((pStats.meleeWeaponSlot))
            {
                if (pStats.selectedWeapon.GetComponent<MeleWeapon>())
                {
                    meleAttack(); // a local function because calling it on the mele weapon would be pointless as their is no need for mele weapons to operate differently in any significant mechanical way.
                }
                }
            else
            {
                // ranged Attacking. Just going to call the attack funcion on the ranged weapon.
            }

        }

        hitColliderOrigin = transform.position;
        hitDirection = charMove.face.transform.forward;
    }
    void meleAttack()
    {
        meleWeap = pStats.meleeWeaponSlot.GetComponent<MeleWeapon>();
        maxHitDistance = meleWeap.range;
        damage = meleWeap.damage;

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
