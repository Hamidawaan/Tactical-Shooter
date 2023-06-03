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
    public PlayerMovement player;
    public Animator animator;

    [Header("Rifle Amunation and Shooting")]
    private float nextTimeToShoot = 0f;
    private int maximumAmunation = 20;
    private int mag = 15;
    private int presentAmunation;
    public float reloadingTime = 1.3f;
    public bool setReloading = false;

    [Header("Rifle Effect")]

    public ParticleSystem mazzleSpark;
    public GameObject woodedEffect;
    public GameObject goreEffect;


    private void Awake()
    {
        presentAmunation = maximumAmunation;
    }

    private void Update()
    {

        if (setReloading)// when we are not reloading
            return;


        if(presentAmunation <= 0) // when ammo will 0 then reloading function will call.
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1")  && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f/fireCharge;  // Time.time is the current time. //   the 1f/firecharge means the rifle shoot raycst will be 0.06
            shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);           
            animator.SetBool("FireWalk", true);
            

        }
        else if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);

        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
            
        }
    }
    void shoot()
    {

        if (mag == 0)
        {
            // show the message the  mag is low
        }


        presentAmunation--; // when player shoot the ammu will decrease

        if(presentAmunation == 0) // when ammu will zero the 1 magazine will decrease(now the 14 mag will remain and then so on)
        {
            mag--;
        }

        mazzleSpark.Play();
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            Objects objects = hitInfo.transform.GetComponent<Objects>();
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if(objects != null)
            {
                objects.objectHitDamage(giveDamage);
                GameObject woodGo = Instantiate(woodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(woodGo, 1f);
            }

            else if(enemy!= null)
            {
                enemy.enemyHitDamage(giveDamage);
                GameObject goreGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreGo, 1f);
            }
        }
    }

    IEnumerator Reload() // this method to stop for specific time
    {
        // in these line of code we take a refernce from playermovement script.
        // after this when player is reloading we can stop their movement and give reloading time. after this we assign the again ammunation.
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading......");
        //Annmation and Audio
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadingTime);
        //Annimation
        animator.SetBool("Reloading", false);
        presentAmunation = maximumAmunation;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3f;
        setReloading = false;



    }



}
