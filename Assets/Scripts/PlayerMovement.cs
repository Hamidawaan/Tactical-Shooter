using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;


    [Header("Player Animator and Gravity")]
    public CharacterController cC;


    [Header("Player jumping and velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    private void Update()
    {
        playerMove();
    }

    void playerMove()   //(normalized) the magnitude of the direction vector is always between 0 and 1.
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float Vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, Vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)//magnitude is to calculate the length of the vector
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;// calculates the target angle based on the direction vector and sets the object's rotation to face that angle in a single line.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime );
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            cC.Move(direction.normalized * playerSpeed * Time.deltaTime);//Time.deltaTime to ensure consistent movement speed across different frame rates. //  that represents the time in seconds
                                                                         //direction.normalized is used to normalize the direction vector before applying it to the player's movement. This ensures that the player moves at a consistent speed
        }

    }
}
