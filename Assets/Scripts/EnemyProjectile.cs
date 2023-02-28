using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject projectileMesh;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10f);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * 500f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            other.GetComponent<Player>().TakeDamage();
        }
        rigidbody.velocity = Vector3.zero;
        projectileMesh.SetActive(false);
        particleSystem.Play();
        Destroy(this.gameObject,0.5f);
    }
}
