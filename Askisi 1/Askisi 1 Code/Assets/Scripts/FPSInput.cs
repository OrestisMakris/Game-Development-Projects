using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    private CharacterController charController;

    public float speed = 7.5f;      // Default speed set to 6
    public float gravity = -9.8f;
    public float jumpSpeed = 4.7f;  // Speed at which the player jumps

    private float verticalSpeed = 0.0f;  // Current vertical movement
    private bool canDoubleJump = false;  // Double jump flag

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        // Check if the player is on the ground
        if (charController.isGrounded)
        {
            // Reset vertical speed when grounded
            verticalSpeed = 0.0f;

            // Allow jump if the space key is pressed
            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
                canDoubleJump = true;  // Enable the ability for a second jump
            }
        }
        else
        {
            // If not grounded, apply gravity
            verticalSpeed += gravity * Time.deltaTime;

            // Allow a single double jump if space is pressed
            if (canDoubleJump && Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
                canDoubleJump = false;  // Disable further jumps
            }
        }

        // Combine horizontal and vertical movement
        Vector3 horizontalMovement = new Vector3(deltaX, 0, deltaZ);
        horizontalMovement = Vector3.ClampMagnitude(horizontalMovement, speed); // Clamp horizontal movement only

        Vector3 movement = horizontalMovement;
        movement.y = verticalSpeed; // Add vertical movement separately

        movement = transform.TransformDirection(movement);
        movement *= Time.deltaTime;

        charController.Move(movement);
    }

}
