using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;
    private bool isMoving = false;
    private bool isJumping = false;
    private bool isFalling = false;
    
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    
    // Update is called once per frame
    void Update()
    {
        // Retrieves the raw value of the input axis named "Horizontal".
        horizontal = Input.GetAxisRaw("Horizontal");
        // Set the animation param "speed" with the raw value on Horizontal axis
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        CheckJumping();
        CheckKeys();
        ChangeSpriteDirection();
    }

    private void FixedUpdate()
    {
        // Defines the velocity of the player on X axis (raw input value) and Y axis (relative to rigidBody attributes)
        rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void ChangeSpriteDirection()
    {
        // If player's sprite is looking to the right and going left or if it is not facing right and going right
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            // Change the state of the isFacingRight boolean
            isFacingRight = !isFacingRight;
            // Flip the player's sprite
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Makes the players goes down faster than falling
    private void goDown()
    {
        // Only available is the player is not on ground
        if (IsGrounded() == false)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -10);
        }
    }

    // Checks if some keys are pressed and do the action linked to the input
    private void CheckKeys(){
        if (Input.GetKeyDown(KeyCode.S))
        {
            goDown();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("LightAttack",true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("LightAttack",false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetBool("HeavyAttack",true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("HeavyAttack",false);
        }
    }

    private void CheckJumping(){
        // If the people is on the ground and press the default button to jump
        if(Input.GetButtonDown("Jump") && IsGrounded()){
            // Makes the character jump
            isJumping = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }
        // WHAT THE HELL DID I DO HERE ? 
        if (Input.GetButtonDown("Jump") && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
        }
        // If the player is falling, the fall is accelerated
        if (rigidBody.velocity.y < 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 1.001f * rigidBody.velocity.y);
        }
    }
}