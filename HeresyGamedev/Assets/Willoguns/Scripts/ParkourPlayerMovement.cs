using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float rbDrag = 6f;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 moveDirection;

    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        ControlDrag();
    }

    private void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    private void ControlDrag()
    {
        rb.drag = rbDrag;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }
}
