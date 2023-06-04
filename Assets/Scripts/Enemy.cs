using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health and Damage")]
    public float enemyHealth = 120f;
    public float presentHealth;
    public float giveDamage = 5f;
    public float enemySpeed;

    [Header("Enemy Things")]
    public Transform lookPoint;
    public GameObject shootingRaycastArea;
    public NavMeshAgent enemyAgent;
    public Transform playerBody;
    public LayerMask playerLayer;
    public Transform spawn;
    public Transform Enemycharacter;

    [Header("Enemy shooting var")]
    public float timebtwShoot;
    bool previouslyShoot;

    [Header("Enemy state")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerinvisionRadius;
    public bool playerinshootingRadius;
    public bool isplayer = false;


    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        presentHealth = enemyHealth;
    }

    private void Update()
    {
        playerinvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerinshootingRadius = Physics.CheckSphere(transform.position,shootingRadius, playerLayer);

        if(playerinvisionRadius && !playerinshootingRadius)
        {
            persuePlayer();
        }
        if (playerinvisionRadius && playerinshootingRadius)
        {
            shootPlayer();
        }

    }

    void persuePlayer()
    {
        if (enemyAgent.SetDestination(playerBody.position))
        {
            // annimation
        }
    }

    private void shootPlayer()
    {
        enemyAgent.SetDestination(transform.position); // stop the enemy player(transform the position)
        transform.LookAt(lookPoint); // the enemy change the face in the player side.

        if (!previouslyShoot)
        {
            RaycastHit hit;
            if(Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("shooting" + hit.transform.name);
                PlayerMovement playerBody = hit.transform.GetComponent<PlayerMovement>();

                if(playerBody!= null)
                {
                    playerBody.playerHitDamage(giveDamage);
                }
            }
        }


        previouslyShoot = true;
        Invoke(nameof(ActiveShooting), timebtwShoot);
    }

    private void ActiveShooting()
    {
        previouslyShoot = false;
    }

    public void enemyHitDamage(float takeDamage)
    {
        presentHealth = presentHealth - takeDamage;
        if (presentHealth <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        enemyAgent.SetDestination(transform.position);
        enemySpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        playerinvisionRadius = false;
        playerinshootingRadius = false;

        // animations
        Debug.Log("Dead");

        yield return new WaitForSeconds(5f);

        Debug.Log("Spawn");
        presentHealth = 120f;
        enemySpeed = 3f;
        shootingRadius = 10f;
        visionRadius = 100f;
        playerinvisionRadius = true;
        playerinshootingRadius = false;

        // animations

        // spawnpoint
        Enemycharacter.transform.position = spawn.transform.position; // assign the spawn points to  the enemy
        persuePlayer();


    }

}
