using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float fireRate = 8f;
    [SerializeField] float ammoSize = 30;
    [SerializeField] float currentAmmo;
    bool hasAmmo = true;
    private float nextTimeToFie = 0f;
    AudioSource audioSource;
    [SerializeField] Transform muzzleLocation;
    [SerializeField] GameObject lazerSpark;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject projectile;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentAmmo = ammoSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmo <= 0)
        {
            hasAmmo = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentAmmo = ammoSize;
            hasAmmo = true;
        }
    }

    public void RayCast()
    {
        if (Time.time >= nextTimeToFie && hasAmmo == true) {
            nextTimeToFie = Time.time + 1f / fireRate;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            currentAmmo--;
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                animator.SetTrigger("WeaponFire");
                Debug.Log(hitInfo.transform.name);
                Instantiate(projectile, muzzleLocation.transform.position, Camera.main.transform.rotation);
                audioSource.Play();
                GameObject hitSpark = Instantiate(lazerSpark, hitInfo.transform.position, Quaternion.identity);
                Destroy(hitSpark, 1f);
                //Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    hitInfo.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }
}
