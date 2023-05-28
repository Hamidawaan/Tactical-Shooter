using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle")]
    public Camera cam;
    public float giveDamage = 10f;
    public float shootingRange = 100f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    void shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position , cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);
        }
    }



}
