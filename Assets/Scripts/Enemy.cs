using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 5f;
    [SerializeField] private bool isEnemeyAlive = true;
    [SerializeField] AudioSource damageAudioSource;
    [SerializeField] Rigidbody[] bodyParts;
    [SerializeField] GameObject explosion;
    [Header("Attack")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform projectileSpawnPoint;
    bool hasAttack = false;
    [SerializeField] float fireRate = 1f;
    [SerializeField] Transform playerLookAtTargetTransfer;
    [SerializeField] AudioSource attackAudioSource;
    [Header("AI")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private LayerMask player;
    //Patroling
    Vector3 walkPos;
    float moveTime = 0f;
    bool walkPoseSet;
    [SerializeField] float walkRange;
    //States
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool isPlayerinSightRange, isPlayerinAttackRange;
    //Item Drop
    [Header("Item Drop")]
    [SerializeField] GameObject healthPickUpPrefab;
    [SerializeField] float healthPickUpSpawnRate;
    // Start is called before the first frame update
    void Start()
    {
        SearchForWalkPose();
    }

    // Update is called once per frame
    void Update()
    {
       //Enemey AI
       SetAIStates();
    }

    private void SetAIStates()
    {
        if(isEnemeyAlive == true)
        {
            isPlayerinSightRange = Physics.CheckSphere(transform.position, sightRange, player);
            isPlayerinAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

            if (isPlayerinSightRange == false && isPlayerinAttackRange == false)
            {
                Patroling();
            }
            else if (isPlayerinSightRange == true && isPlayerinAttackRange == false)
            {
                ChasePlayer();
            }
            else if (isPlayerinSightRange == true && isPlayerinAttackRange == true)
            {
                Attack();
            }
        }
    }

    private void Patroling()
    {
        if (walkPoseSet == true && isEnemeyAlive == true)
        {
            navMeshAgent.SetDestination(walkPos);
        }
        else
        {
            SearchForWalkPose();
        }

        Vector3 distanceToWalk = transform.position - walkPos;
        //Debug.Log("Walk Distance" + distanceToWalk.magnitude);
        //Debug.Log("Velocity" + navMeshAgent.velocity.magnitude);

        //Check if stuck
        moveTime += Time.deltaTime;
        if (navMeshAgent.velocity.magnitude < 1f && moveTime > 1f)
        {
            walkPoseSet = false;
            moveTime = 0;
        }
    }


    private void SearchForWalkPose()
    {
        float randomX = Random.Range(-walkRange, walkRange);
        float randomZ = Random.Range(-walkRange, walkRange);
        walkPos = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPoseSet = true;
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(playerLookAtTargetTransfer.position);
    }

    private void Attack()
    {
        transform.LookAt(playerLookAtTargetTransfer.position);
        if (hasAttack == false)
        {
            attackAudioSource.Play();
            Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            hasAttack = true;
            Invoke(nameof(ResetAttack), fireRate);
        }
    }

    private void ResetAttack()
    {
        hasAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage()
    {
        damageAudioSource.Play();
        health--;
        if (health == 0)
        {
            Death();
        }
    }

    private void ItemDrop(GameObject item, float itemDropRate)
    {
        float drawn = Random.Range(0f, 100f);
        if (drawn <= itemDropRate)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

    public void Death()
    {
        isEnemeyAlive = false;
        explosion.SetActive(true);
        float explosionForce = Random.Range(100f, 350f);
        float explosionRadius = Random.Range(20f, 50f);
        foreach (Rigidbody item in bodyParts)
        {
            item.isKinematic = false;
            item.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            item.useGravity = true;
        }
        //Health Item
        ItemDrop(healthPickUpPrefab, healthPickUpSpawnRate); 
        Destroy(this.gameObject, 10f);
    }


}
