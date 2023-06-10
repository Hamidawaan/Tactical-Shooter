using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    [Header("Player Health and Damage")]
    public float PlayerHealth = 120f;
    public float presentHealth;
    public float giveDamage = 5f;
    public float PlayerSpeed;

    [Header("Player Things")]
    public Transform lookPoint;
    public GameObject shootingRaycastArea;
    public NavMeshAgent PlayerAgent;
    public Transform enemyBody;
    public LayerMask enemyLayer;
    public Transform spawn;
    public Transform Playercharacter;

    [Header("Player shooting var")]
    public float timebtwShoot;
    bool previouslyShoot;

    [Header("Enmey Annimation ad spark effect")]
    public Animator animator;
    public ParticleSystem mazleSpark;

    [Header("Player state")]
    public float visionRadius;
    public float shootingRadius;
    public bool enemyinvisionRadius;
    public bool enemyinshootingRadius;



    private void Awake()
    {
        PlayerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        presentHealth = PlayerHealth;
    }

    private void Update()
    {
        enemyinvisionRadius = Physics.CheckSphere(transform.position, visionRadius, enemyLayer);
        enemyinshootingRadius = Physics.CheckSphere(transform.position, shootingRadius, enemyLayer);

        if (enemyinvisionRadius && !enemyinshootingRadius)
        {
            persueEnemy();
        }
        if (enemyinvisionRadius && enemyinshootingRadius)
        {
            shootEnemy();
        }

    }

    void persueEnemy()
    {
        if (PlayerAgent.SetDestination(enemyBody.position))
        {
            // annimation
            animator.SetBool("Running", true);
            animator.SetBool("Shooting", false);

        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Shooting", false);
        }
    }

     private void  shootEnemy()
    {
        PlayerAgent.SetDestination(transform.position); // stop the Player player(transform the position)
        transform.LookAt(lookPoint); // the Player change the face in the player side.

        if (!previouslyShoot)
        {
            mazleSpark.Play();

            RaycastHit hit;
            if (Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("shooting" + hit.transform.name);
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.enemyHitDamage(giveDamage);

                }
            }

            

            animator.SetBool("Running", false);
            animator.SetBool("Shooting", true);
        }


        previouslyShoot = true;
        Invoke(nameof(ActiveShooting), timebtwShoot);
    }

    private void ActiveShooting()
    {
        previouslyShoot = false;
    }

    public void PlayerAiHitDamage(float takeDamage)
    {
        presentHealth = presentHealth - takeDamage;
        if (presentHealth <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        PlayerAgent.SetDestination(transform.position);
        PlayerSpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        enemyinvisionRadius = false;
        enemyinshootingRadius = false;
        animator.SetBool("Die", true);
        animator.SetBool("Running", false);
        animator.SetBool("Shooting", false);
        // animations
        Debug.Log("Dead");

        yield return new WaitForSeconds(5f);

        Debug.Log("Spawn");
        presentHealth = 120f;
        PlayerSpeed = 3f;
        shootingRadius = 10f;
        visionRadius = 100f;
        enemyinvisionRadius = true;
        enemyinshootingRadius = false;

        // animations
        animator.SetBool("Running", true);
        animator.SetBool("Die", false);
        // spawnpoint
        Playercharacter.transform.position = spawn.transform.position; // assign the spawn points to  the Player
        persueEnemy();


    }
}
