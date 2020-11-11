using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    public Transform muzzleTransform;
    //public GameObject bulletPrefab;
    public float bulletVelocity = 100.0f;
    public float fireRate = 3.0f;
    public float impactForce = 100.0f;
    public float damage = 10.0f;
    public float range = 100.0f;

    public Camera tpsCamera;
    public VisualEffect muzzleFlash;
    public GameObject impactEffect;

    public int maxBulletsCount = 10;
    public int currentBulletCount;


    private float nextTimeToFire = 0.0f;
    private float dividedFireRate = 0.0f;



    private void Awake()
    {
        Reload();
    }

    private void Start()
    {
        muzzleFlash.Stop();
        dividedFireRate = 1.0f / fireRate;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + dividedFireRate;
            Shoot();
        }
    }
    public void Shoot()
    {
        if (currentBulletCount > 0)
        {
            //GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.identity);
            //Rigidbody rb = bullet.GetComponent<Rigidbody>();
            //rb.AddForce(muzzleTransform.forward * bulletVelocity, ForceMode.Impulse);
            //currentBulletCount--;
            muzzleFlash.Play();
            RaycastHit hit;

            if (Physics.Raycast(tpsCamera.transform.position, tpsCamera.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                IDamageable target = hit.transform.GetComponent<IDamageable>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                if (!hit.transform.gameObject.CompareTag("Player"))
                {
                    GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2.0f);
                }
            }

            currentBulletCount--;
            UpdateUI();
        }
    }

    public void Reload()
    {
        currentBulletCount = maxBulletsCount;
        UpdateUI();
    }


    public void UpdateUI()
    {
        ServiceLocator.Get<UIManager>().UpdateBulletText(currentBulletCount + "/" + maxBulletsCount);
    }


}
