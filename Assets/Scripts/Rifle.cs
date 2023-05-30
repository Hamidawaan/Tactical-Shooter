using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle")]
    public Camera cam;
    public float giveDamage = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;

    [Header("Rifle Annimation and Shooting")]
    private float nextTimeToShoot = 0f;

    private void Update()
    {
        if (Input.GetButton("Fire1")  && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f / fireCharge;  // Time.time is the current time. //   the 1f/firecharge means the rifle shoot raycst will be 0.06
            shoot();
        }
    }
    void shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            Objects objects = hitInfo.transform.GetComponent<Objects>();
            if(objects != null)
            {
                objects.objectHitDamage(giveDamage);
            }
        }
    }



}
