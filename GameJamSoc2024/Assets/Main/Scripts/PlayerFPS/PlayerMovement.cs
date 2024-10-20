using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame.Interfaces.EnemiesInterfaces;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("Public Attributes")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float jumpForce = 8f;
    public float groundCheckDistance = 0.4f;

    [Header("Ground Detection")]
    public LayerMask groundMask;
    public Transform groundCheck;

    private Rigidbody rb;
    private Vector3 velocity;
    private bool isGrounded;


    public event Action<IDamageable> OnDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    void Move()
    {
        // Get input for movement (WASD / arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement direction vector
        Vector3 moveDirection = Vector3.right * moveX + Vector3.forward * moveZ;

        // Check for sprinting input
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0));
    }

    public Transform GetTransform() => transform;

    public void DoDamage(int damage, bool affectFlying = false)
    {
        throw new NotImplementedException();
    }


    public void Heal(int healAmount)
    {
        throw new NotImplementedException();
    }
}
