using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform target;
    public Transform aim;
    public Transform head;
    public GameObject LaserPrefab;


    public float reloadTime = 1.0f;
    public float turnSpeed = 5.0f;
    public float firePauseTime = 0.25f;
    public float range = 30.0f;

    public Transform muzzle;

    private float nextFireTime;
    private float nextMoveTime;

    private void Awake()
    {
        target = GameObject.Find("Spine").transform;
    }

    void Start()
    {
    }

    public float GetNextFireTime()
    {
        return nextFireTime;
    }
    public float GetReloadTime()
    {
        return reloadTime;
    }

    void Update()
    {
        AimFire();
    }

    void AimFire()
    {
        if (Vector3.Distance(transform.position, target.position) < range)
        {
            if (Time.time >= nextMoveTime)
            {
                aim.LookAt(target);
                aim.eulerAngles = new Vector3(aim.eulerAngles.x, aim.eulerAngles.y, 0);
                head.rotation = Quaternion.Lerp(head.rotation, aim.rotation, Time.deltaTime * turnSpeed);
            }

            if (Time.time >= nextFireTime)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        nextFireTime = Time.time + reloadTime;
        nextMoveTime = Time.time + firePauseTime;

        GameObject laser = Instantiate(LaserPrefab, muzzle.position, transform.rotation) as GameObject;
        laser.GetComponent<LaserBehavior>().setTarget(target.position);
        Destroy(laser, 1.0f);
    }
}
