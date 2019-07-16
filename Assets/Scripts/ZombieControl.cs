using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour
{
    public ZombieSpawner spawner;
    public GameObject player1;
    public GameObject player2;
    private Stats zombieStats;
    private GameObject target;
    private NavMeshAgent agent;
    private NavMeshPath pathA;
    private NavMeshPath pathB;
    public float attackDamage;
    private float attackCoolDown;
    public float attackCoolDownMax = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        zombieStats = GetComponent<Stats>();
        agent = GetComponent<NavMeshAgent>();
        //player1 = GameManager.player
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieStats.health <= 0)
        {
            Die();
        }
        else
        {
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
                transform.LookAt(target.transform);
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

            if (attackCoolDown > 0)
            {
                attackCoolDown -= Time.deltaTime;
            }
        }
    }
    void Attack( GameObject player)
    {
        Stats playerStats = player.GetComponent<Stats>();
        playerStats.TakeDamage(attackDamage);
        player.GetComponent<PlayerControl>().KnockBack(attackDamage, transform.forward);
    }
    public void KnockBack(float dmg, Vector3 dir)
    {
        transform.position += (dir * dmg * 0.1f);
    }
    void Die()
    {
        spawner.CurrentZombies.Remove(gameObject);
        // We will elaborate on this later.  Plans -- make them fall over, then slowly sink them through the ground
        Destroy(gameObject);
    }
}
