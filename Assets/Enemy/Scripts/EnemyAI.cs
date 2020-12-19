//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;

    float distanceToPlayer = Mathf.Infinity;

    bool isProvked = false;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (isProvked)
        {
            EngagePlayer();
        }

        else if (distanceToPlayer <= chaseRange)
        {
            // if player is visible
            isProvked = true;
        }
        
        
    }

    
    private void EngagePlayer()
    {
        FacePlayer();

        if (distanceToPlayer >= navMeshAgent.stoppingDistance)
        {
            ChasePlayer();
        }

        if (distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {

        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    
}
