using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
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
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
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
    void Attack( GameObject player)
    {
        Stats playerStats = player.GetComponent<Stats>();
        playerStats.TakeDamage(attackDamage);
    }
}
