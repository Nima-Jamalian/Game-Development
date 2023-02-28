using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Pramater")]
    [SerializeField] float fireRate = 8f;
    [SerializeField] float ammoSize = 30;
    [SerializeField] float currentAmmo;
    bool hasAmmo = true;
    private float nextTimeToFie = 0f;
    [SerializeField] float currentAmmoPrecentage;
    bool isColorChnageLerpActive = false;
    
    [Header("Weapon VFX")]
    [SerializeField] Transform muzzleLocation;
    [SerializeField] GameObject lazerSparkGreen,lazerSparkRed;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject projectile;
    [SerializeField] Material weaponMaterial;
    [SerializeField] Color WeaponMagFull;
    [SerializeField] Color WeaponMagHlaf;
    [SerializeField] Color WeaponMagEmpty;
    
    Animator animator;

    [Header("Weapon Audio")]
    [SerializeField] AudioClip weaponFire;
    [SerializeField] AudioClip weaponReload;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentAmmo = ammoSize;
        weaponMaterial.SetColor("_EmissionColor", WeaponMagFull);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(WeaponReload());
        }

        CheckWeaponAmmuStatusAndUpdateColor();

    }



    public void Shooting()
    {
        if (Time.time >= nextTimeToFie && hasAmmo == true) {
            //Visial Effects
            //Update Fire Rate
            nextTimeToFie = Time.time + 1f / fireRate;
            //Update Ammu
            currentAmmo--;
            //Muzzle Flash Effect
            muzzleFlash.Play();
            //Shooting Audio
            audioSource.clip = weaponFire;
            audioSource.Play();
            //Fire Projectile
            Instantiate(projectile, muzzleLocation.transform.position, Camera.main.transform.rotation);
            //Weapon Recoil Animation
            animator.SetTrigger("WeaponFire");

            //Raycast
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {       

                //Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    GameObject hitSpark = Instantiate(lazerSparkRed, hitInfo.point, Quaternion.identity);
                    Destroy(hitSpark, 1f);
                    hitInfo.transform.GetComponent<Enemy>().TakeDamage();
                } else
                {
                    GameObject hitSpark = Instantiate(lazerSparkGreen, hitInfo.point, Quaternion.identity);
                    Destroy(hitSpark, 1f);
                }
            }
        }
    }

    IEnumerator WeaponReload()
    {
        hasAmmo = false;
        StartCoroutine(WeaponColorChangeLerp(WeaponMagFull, 1f));
        animator.SetTrigger("WeaponReload");
        audioSource.clip = weaponReload;
        audioSource.Play();
        yield return new WaitForSeconds(1f);
        audioSource.Stop();
        currentAmmo = ammoSize;
        hasAmmo = true;
    }

    private void CheckWeaponAmmuStatusAndUpdateColor()
    {
        if (currentAmmo <= 0)
        {
            hasAmmo = false;
        }

        currentAmmoPrecentage = (currentAmmo / ammoSize) * 100;
        if (currentAmmoPrecentage < 55 && currentAmmoPrecentage > 20 && isColorChnageLerpActive == false && hasAmmo == true)
        {
            //turn yello
            if (weaponMaterial.GetColor("_EmissionColor") != WeaponMagHlaf)
            {
                StartCoroutine(WeaponColorChangeLerp(WeaponMagHlaf, 1f));
            }
        }
        else if (currentAmmoPrecentage < 20 && isColorChnageLerpActive == false && hasAmmo == true)
        {
            if (weaponMaterial.GetColor("_EmissionColor") != WeaponMagEmpty)
            {
                //turn red
                StartCoroutine(WeaponColorChangeLerp(WeaponMagEmpty, 1f));
            }
        }
    }

    IEnumerator WeaponColorChangeLerp(Color endValue, float duration)
    {
        Debug.Log("I am being called");
        isColorChnageLerpActive = true;
        float time = 0;
        Color startValue = weaponMaterial.GetColor("_EmissionColor");

        while (time < duration)
        {
            weaponMaterial.SetColor("_EmissionColor", Color.Lerp(startValue, endValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }

        weaponMaterial.SetColor("_EmissionColor", endValue);
        isColorChnageLerpActive = false;
    }
}
