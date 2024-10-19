using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float jumpForce = 8f;
    public float gravity = -20f;  // Increased gravity for quicker fall
    public float groundCheckDistance = 0.4f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public LayerMask groundMask;
    public Transform groundCheck;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        // Reset velocity when grounded to ensure constant contact with the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Handle movement
        Move();

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Apply gravity each frame
        ApplyGravity();

        // Apply the velocity to the CharacterController
        controller.Move(velocity * Time.deltaTime);
    }

    void Move()
    {
        // Get input for movement (WASD / arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement direction vector
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Check for sprinting input
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Move the player based on the current speed
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    void Jump()
    {
        // Apply jump force
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    void ApplyGravity()
    {
        // Apply gravity force over time
        velocity.y += gravity * Time.deltaTime;
    }
}
