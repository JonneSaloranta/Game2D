using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    [SerializeField] float jumpForce;
    [SerializeField] float movementSpeed;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    private PlayerControls playerControls;

    private float horizontalInput;
    private bool isFacingRight;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        playerControls = new PlayerControls();
        playerControls.Movement.Jump.performed += ctx => Jump();
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

    private bool IsGrounded() {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    private void OnEnable() {
        playerControls.Movement.Enable();
    }

    private void OnDisable() {
        playerControls.Movement.Disable();
    }
}
