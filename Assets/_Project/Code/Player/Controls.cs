using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    [SerializeField] float jumpForce;

    [SerializeField] private LayerMask platformLayerMask;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    private PlayerControls playerControls;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        playerControls = new PlayerControls();
        playerControls.Movement.Jump.performed += ctx => Jump();
    }

    public void Jump() {
        if (!isGrounded()) return;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private bool isGrounded() {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    private void OnEnable() {
        playerControls.Movement.Enable();
    }

    private void OnDisable() {
        playerControls.Movement.Disable();
    }
}
