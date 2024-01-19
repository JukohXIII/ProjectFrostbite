using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;

    private float speed = 8f;

    private float jumpingPower = 20f;

    private bool isFacingRight = true;
    private bool isMoving = false;
    private bool isJumping = false;
    private bool isFalling = false;

    [SerializeField] private Rigidbody2D rigidBody;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;
    
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal)
        {
            isMoving = true;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }
        if (Input.GetButtonDown("Jump") && rigidBody.velocity.y > 0f)
        {
            isFalling = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }
        Flip();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
