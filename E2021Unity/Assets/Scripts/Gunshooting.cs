using System.Collections;
using UnityEngine;

public class Gunshooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public int maxAmmo = 10;
    private int currentAmmo = -1;
    public float reloadtime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffet;

    private float nextTimeToFire = 0f;

    public Animator animator;

    private void Start()
    {
        if(currentAmmo == -1)
            currentAmmo = maxAmmo;
    }

   void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0f)
        {
            StartCoroutine( Reload() );
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time * 1f/fireRate;
            Shoot();
        }

        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading");

            animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadtime - .25f);
            animator.SetBool ("Reloading", false);
            yield return new WaitForSeconds(.25f);

            currentAmmo = maxAmmo;
            isReloading = false;
        }

        void Shoot()
        {
            muzzleFlash.Play();

            currentAmmo--; 

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
               Debug.Log(hit.transform.name);

               Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                };

                GameObject impactGO = Instantiate(impactEffet, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }      
         }
    }
}
