using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour
{   
    //these get set by the spawner when the zombie is instantiated.
    public ZombieSpawner spawner;
    public GameObject player1; 
    public GameObject player2;

    //these don't
    private Stats zombieStats;
    private GameObject target;
    private NavMeshAgent agent;
    private NavMeshPath pathA;
    private NavMeshPath pathB;
    public float attackDamage;
    private float attackCoolDown;
    public float attackCoolDownMax = 5.0f;
    private float knockBackTime;
    public float knockBackMultiplier;
    public float MaxknockBackTime;
    public float KnockBackSpeed;
    private float defaultSpeed;
    private float defaultAngularSpeed;
    void Start()
    {
        zombieStats = GetComponent<Stats>();
        agent = GetComponent<NavMeshAgent>();
        defaultAngularSpeed = agent.angularSpeed;
        defaultSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieStats.health <= 0)
        {
            Die();
        }
        else if (knockBackTime<=0)
        {
            agent.speed = defaultSpeed;
            agent.angularSpeed = defaultAngularSpeed;
            if (Vector3.Distance(player1.transform.position, transform.position) > Vector3.Distance(player2.transform.position, transform.position))
            {
                target = player2;
            }
            else
            {
                target = player1;
            }
            agent.destination = target.transform.position;
            agent.autoTraverseOffMeshLink = true;

            if (Vector3.Distance(transform.position, target.transform.position) <= 1.8f)
            {
                agent.isStopped = true;
                LookAt(target.transform);
                if (attackCoolDown <= 0)
                {
                    Attack(target);
                    attackCoolDown = attackCoolDownMax;
                }
            }
            else
            {
                agent.isStopped = false;
            }


        }
        else if (knockBackTime>0)
        {
            agent.speed = KnockBackSpeed;
            agent.angularSpeed = 0;
            agent.isStopped = false;
            knockBackTime -= Time.deltaTime;
            LookAt(target.transform);
        }
        if (attackCoolDown > 0)
        {
            attackCoolDown -= Time.deltaTime;
        }
    }
    void LookAt(Transform t)
    {
        Vector3 tSameHeight;
        tSameHeight.x = t.position.x;
        tSameHeight.z = t.position.z;
        tSameHeight.y = transform.position.y;
        
        transform.LookAt(tSameHeight);

    }
    void LookAt(Vector3 v)
    {
        Vector3 tSameHeight;
        tSameHeight.x = v.x;
        tSameHeight.z = v.z;
        tSameHeight.y = transform.position.y;

        transform.LookAt(tSameHeight);
    }
    void Attack( GameObject player)
    {
        Stats playerStats = player.GetComponent<Stats>();
        playerStats.TakeDamage(attackDamage);
        //player.GetComponent<PlayerControl>().KnockBack(attackDamage, transform.forward);
    }
    public void KnockBack(float dmg, Vector3 dir)
    {
        agent.destination = transform.position + (dir * dmg * knockBackMultiplier);
        knockBackTime = MaxknockBackTime;
    }
    void Die()
    {
        spawner.CurrentZombies.Remove(gameObject);
        // We will elaborate on this later.  Plans -- make them fall over, then slowly sink them through the ground
        Destroy(gameObject);
    }
}
