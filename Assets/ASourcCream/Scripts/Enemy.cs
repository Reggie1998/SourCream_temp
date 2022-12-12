using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    [SerializeField]
    public Transform player;

    //Patrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public LayerMask whatIsGround, whatIsPlayer;

    public bool once;

    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        once = false;
    }

    private void Update()
    {   
        //TODO: Would be cool to add a range indicator
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) {
            Patroling();
        };
        if (playerInSightRange && !playerInAttackRange) {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange) { 
            AttackPlayer();
        }

    }

    private void Patroling()
    {
        if (!walkPointSet) {
            SearchWalkPoint();
        } else {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walk point reached
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked) {
            //attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.forward * 8f, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(Attack), timeBetweenAttacks);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void Attack() 
    {   var positionWithOffset = new Vector3(transform.position.x, transform.position.y + 25.0f, transform.position.z + 20.0f);
        
        if (!once) {
            once = true;
            ProjectileSpawner.shootSimpleProjectile(player.position, positionWithOffset, projectile, 200000.0f);
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(246, 182, 215, 0.4f);
        Gizmos.DrawSphere(transform.position, sightRange);
        Gizmos.color = new Color(100, 100, 100, 0.4f);
        Gizmos.DrawSphere(transform.position, attackRange);
    }
   
}
