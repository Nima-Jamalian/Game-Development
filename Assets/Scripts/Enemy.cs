using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 5f;
    private bool isEnemeyAlive = true;
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
    private NavMeshAgent navMeshAgent;
    //Patroling
    [SerializeField] Vector3 walkPos;
    [SerializeField] bool walkPoseSet;
    [SerializeField] float walkRange;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SearchForWalkPose();
    }

    // Update is called once per frame
    void Update()
    {
        //Loook at the player
        transform.LookAt(playerLookAtTargetTransfer.position);
        //Patroling();
        //ChasePlayer();
        //Attack();
    }

    private void Patroling()
    {
        if(walkPoseSet == true)
        {
            navMeshAgent.SetDestination(walkPos);
        } else
        {
            SearchForWalkPose();
        }

        Vector3 distanceToWalk = transform.position - walkPos;
        //Debug.Log(distanceToWalk.magnitude);
        Debug.Log("Velocity" + navMeshAgent.velocity.magnitude);
        if(distanceToWalk.magnitude < 5f)
        {
            if (navMeshAgent.velocity.magnitude < 1f)
            {
                walkPoseSet = false;
            }
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
        if(isEnemeyAlive == true)
        {
            navMeshAgent.SetDestination(playerLookAtTargetTransfer.position);
        }
    }

    private void Attack()
    {
        if(isEnemeyAlive == true)
        {
            if(hasAttack == false)
            {
                attackAudioSource.Play();
                Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                hasAttack = true;
                Invoke(nameof(ResetAttack), fireRate);
            }
        }
    }

    private void ResetAttack()
    {
        hasAttack = false;
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

    public void Death()
    {
        isEnemeyAlive = false;
        explosion.SetActive(true);
        float explosionForce = Random.Range(100f, 350f);
        float explosionRadius = Random.Range(20f, 50f);
        foreach(Rigidbody item in bodyParts)
        {
            item.isKinematic = false;
            item.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            item.useGravity = true;
        }
        Destroy(this.gameObject, 10f);
    }
}
