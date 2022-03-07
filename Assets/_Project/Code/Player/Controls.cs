using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    [SerializeField] float jumpForce;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        PlayerControls playerControls = new PlayerControls();
        playerControls.Movement.Enable();
        playerControls.Movement.Jump.performed += ctx => Jump();
    }

    public void Jump() {
        if (!isGrounded()) return;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump", this);
    }

    private bool isGrounded() {
        float extraHeightText = .01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeightText);
        Color rayColor;
        if (raycastHit.collider != null) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor, 5f);

        return raycastHit.collider != null;

    }
}
