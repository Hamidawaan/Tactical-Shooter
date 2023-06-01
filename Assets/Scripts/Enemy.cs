using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health and Damage")]
    public float enemySpeed;

    [Header("Enemy Things")]
    public NavMeshAgent enemyAgent;
    public Transform playerBody;
    public LayerMask playerLayer;


    [Header("Enemy state")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerinvisionRadius;
    public bool playerinshootingRadius;
    public bool isplayer = false;


    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerinvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerinshootingRadius = Physics.CheckSphere(transform.position,shootingRadius, playerLayer);

        if(playerinvisionRadius && !playerinshootingRadius)
        {
            persuePlayer();
        }

    }

    void persuePlayer()
    {
        if (enemyAgent.SetDestination(playerBody.position))
        {
            // annimation
        }
    }

}
