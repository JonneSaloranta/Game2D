using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    [Header("Jumping")]
    [SerializeField] float jumpForce;
    [SerializeField] float movementSpeed;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    [Header("Ground check")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckVector;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    private PlayerControls playerControls;

    private float horizontalInput;
    private bool isFacingRight = true;

    private bool isJumping;
    private bool isLowJumping;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        playerControls = new PlayerControls();
        playerControls.Movement.Jump.performed += ctx => Jump();
        playerControls.Movement.Jump.canceled += ctx => isLowJumping = true;
    }

    private void Update() {
        horizontalInput = playerControls.Movement.MovementHorizontal.ReadValue<float>();
    }

    private void FixedUpdate() {

        rb.velocity = new Vector2(movementSpeed * horizontalInput, rb.velocity.y);

        if (!isFacingRight && horizontalInput > 0) {
            ChangeFacingDirection();
        } else if (isFacingRight && horizontalInput < 0) {
            ChangeFacingDirection();
        }

        FasterFall();
    }

    public void FasterFall() {
        if (IsGrounded()) isLowJumping = false;

        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Jump() {
        if (!IsGrounded()) return;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void ChangeFacingDirection() {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public bool IsGrounded() {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckVector, 0, groundLayerMask);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckVector);
    }

    private void OnEnable() {
        playerControls.Movement.Enable();
    }

    private void OnDisable() {
        playerControls.Movement.Disable();
    }
}
