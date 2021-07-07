using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourPlayerMovement : MonoBehaviour
{
    [SerializeField] private ParkourWallRunning wallRun;
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private Rigidbody rb;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float wallRunSpeed = 10f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float wallRunAccelMultiplier;
    public bool isSprinting { get; set; }
    public bool canSprint { get; set; }

    [Header("KeyBinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    public bool doubleJump { private get; set; }

    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 1f;

    private float horizontalMovement;
    private float verticalMovement;
    private float playerHeight = 2;

    private bool isGrounded;
    private float groundDistance = 0.4f;
    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;
    private RaycastHit slopeHit;

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded)
        {
            doubleJump = true;
        }

        MyInput();
        ControlDrag();
        ControlSpeed();

        if(Input.GetKeyDown(jumpKey))
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else if(doubleJump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            doubleJump = false;
        }
    }

    private void ControlSpeed()
    {
        if(Input.GetKey(sprintKey) && isGrounded && canSprint)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            isSprinting = true;
        }
        else if(wallRun.isWallRun)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, wallRunSpeed, (acceleration * wallRunAccelMultiplier) * Time.deltaTime);
            isSprinting = false;
        }
        else if(isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            isSprinting = false;
        }
    }

    private void ControlDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else if(!isGrounded)
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if(isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}
