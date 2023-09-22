using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Health Things")]
    public float playerHealth = 50f;
    public float presentHealth;
    public HealthBar healthBar;
    //public NavMeshAgent PlayerAgent;
    bool isPlayerAlive = true;
    //public Transform Player;




    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float currentPlayerSpeed = 0f;
    public float playerSprint = 3f;
    public float currentplayerSprint = 0f;
    public Transform playerCharacter;
    public Transform spawn;




    [Header("Player Camera")]
    public Transform playerCamera;



    [Header("Player Animator and Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player jumping and velocity")]
    public float jumpRange = 1f;
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;


    private void Start()
    {
        
        presentHealth = playerHealth;
        // healthBar.GiveFullHealth(playerHealth);
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        







        //if the character is on a surface, applies a downward force to keep it grounded,
        //updates the velocity with gravity, and finally moves the character based on the updated velocity.
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        playerMove();
        Jump();
        Sprint();
    }

    void playerMove()   //(normalized) the magnitude of the direction vector is always between 0 and 1.
    {



        animator.SetBool("Walk", true);
        animator.SetBool("Running", false);
        animator.SetTrigger("Jump");
        animator.SetBool("Idle", false);
        animator.SetBool("AimWalk", false);
        animator.SetBool("IdleAim", false);



        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float Vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, Vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)//magnitude is to calculate the length of the vector
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;// calculates the target angle based on the direction vector and sets the object's rotation to face that angle in a single line.  
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);// used to smoothly rotate objects, such as a character or camera, to a desired angle over time,


            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);//Time.deltaTime to ensure consistent movement speed across different frame rates. //  that represents the time in seconds
            currentPlayerSpeed = playerSpeed;                                                            //direction.normalized is used to normalize the direction vector before applying it to the player's movement. This ensures that the player moves at a consistent speed
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetTrigger("Jump");

            animator.SetBool("AimWalk", false);
            currentPlayerSpeed = 0f;

        }


    }
    void Sprint()   //(normalized) the magnitude of the direction vector is always between 0 and 1.
    {

        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) && onSurface)
        {


            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float Vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, Vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)//magnitude is to calculate the length of the vector
            {
                animator.SetBool("Running", true);
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                animator.SetBool("IdleAim", false);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;// calculates the target angle based on the direction vector and sets the object's rotation to face that angle in a single line.  
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);// used to smoothly rotate objects, such as a character or camera, to a desired angle over time,


                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);//Time.deltaTime to ensure consistent movement speed across different frame rates. //  that represents the time in seconds
                currentplayerSprint = playerSprint;                                                            //direction.normalized is used to normalize the direction vector before applying it to the player's movement. This ensures that the player moves at a consistent speed

            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                currentplayerSprint = 0f;
            }

        }

    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Walk", false);

            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);

        }
        else
        {
            animator.ResetTrigger("Jump");
        }
    }


    public void playerHitDamage(float takeDamage)
    {
        presentHealth = presentHealth - takeDamage;
        //  healthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            // playerDie();
            PlayerDie();
            
           // StartCoroutine(Respawn());

        }
    }

    private void PlayerDie()
   {
        // Cursor.lockState = CursorLockMode.None;

        //  Other.Destroy(gameObject);
        //       Destroy(gameObject);
        //  StartCoroutine(Respawn());
        Debug.Log("Player Die");
        Destroy(gameObject);




    }
  /* IEnumerator Respawn()
   {
        playerSpeed = 0f;
        Debug.Log("Player die");
        animator.SetBool("Die", true);
        // animator.SetBool("Fire", false);
       // animator.SetBool("Walk", false);

        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(4f);
       
        Debug.Log("Spawn");
        playerSpeed = 1.9f;
        animator.SetBool("Die", false);
      //  animator.SetBool("Idle", false);

        yield return new WaitForEndOfFrame(); // Wait for end of frame before enabling the idle animation

        animator.SetBool("Idle", true);
      //  animator.SetBool("Walk", true);
        // Enable the idle animation
        animator.SetBool("Die", false);


        playerCharacter.transform.position = spawn.transform.position;
       Cursor.lockState = CursorLockMode.None;

    }*/
    
}