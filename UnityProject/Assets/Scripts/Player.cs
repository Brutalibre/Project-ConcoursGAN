using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public float moveSpeed = 4f;
    public float moveSensitivity = 10f;
    public float jumpForce = 700f;
    public ColorCollector colorCollector;
    public LayerMask decorLayer;
    public float allowedDistanceToDecor = .05f;

    private Rigidbody rb;
    private bool isBouncing = false;
    private float bounceDuration = 0f;
    private float minBounceDuration = 0.1f;
    private bool isMinBoucing = false;
    private bool canMove = true;
    private bool canJump = true;
    private bool isJumping = false;
    private float minJumpDuration = 0.1f;
    private float jumpDuration = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player can and wants to jump
        CheckJump();

        // Check if the player is bouncing, and if so update the bounce state
        CheckBounce();
    }

    void FixedUpdate()
    {
        // Move the player according to the user input
        Move();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Enemy)
        {
            Die();
        }
    }

    void Die()
    {
        colorCollector.BurstColors();
    }
    
    void Move()
    {
        if (!canMove)
            return;
        
        // Read direction input from player
        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * moveSensitivity;

        // Stop here if no input from player
        if (horizontalMove == 0)
            return;

        // Cancel move if playing is against decor
        Vector3 direction = horizontalMove > 0 ? Vector3.right : Vector3.left;
        if (isAgainstDecor(direction))
            return;
        
        // Move player 
        rb.AddForce(new Vector3(horizontalMove, 0f, 0f));
        
        // Make sure player is not moving too fast
        if (Mathf.Abs(rb.velocity.x) > moveSpeed)
        {
            Vector3 newVelocity = rb.velocity;
            newVelocity.x = newVelocity.x > 0 ? moveSpeed : -moveSpeed;
            
            rb.velocity = newVelocity;
        }
    }

    bool isAgainstDecor(Vector3 direction)
    {
        // Check if player is against wall or something static
        // To do so, perform trois raycast going from the top, middle and bottom of the player
        Vector3 raycastOrigin_middle = transform.position;

        float halfPlayerHeight = transform.lossyScale.y / 2f;
        
        Vector3 raycastOrigin_top = raycastOrigin_middle;
        raycastOrigin_top.y += halfPlayerHeight;
        
        Vector3 raycastOrigin_bottom = raycastOrigin_middle;
        raycastOrigin_bottom.y -= halfPlayerHeight;

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin_middle, direction, out hit, 5, decorLayer)
            || Physics.Raycast(raycastOrigin_top, direction, out hit, 5, decorLayer)
            || Physics.Raycast(raycastOrigin_bottom, direction, out hit, 5, decorLayer))
        {
            float distanceToOutside = transform.lossyScale.x / 2f;
            
            // Cancel move if player is against decor
            if(hit.distance <= distanceToOutside + allowedDistanceToDecor)
                return true;
        }

        return false;
    }

    public void Bounce()
    {
        isBouncing = true;
        isMinBoucing = true;
        bounceDuration = 0f;
    }

    bool IsDoneBouncing()
    {
        return rb.velocity == Vector3.zero && !isMinBoucing;
    }

    void CheckBounce()
    {
        if (isBouncing)
        {
            // Update bounce time
            bounceDuration += Time.deltaTime;
            
            // The min bounce duration makes sure the player has started bouncing, because
            // the velocity remains at 0 a little while before the AddForce actually occurs.
            if(bounceDuration >= minBounceDuration)
                isMinBoucing = false;
            
            // The player may move again once the bounce is over
            if (IsDoneBouncing())
            {
                isBouncing = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                canMove = true;
            }
            else
                canMove = false;
        }
    }

    public void EnableJump(bool enabled)
    {
        canJump = enabled;
    }

    void CheckJump()
    {
        // The player has not unlocked the jump action yet (orange fragment)
        if (!canJump)
            return;

        // The player cannot jump while bouncing
        if (isBouncing)
            return;

        // Wait for the player to reach the ground to jump again
        if (isJumping)
        {
            // Update jump time
            jumpDuration += Time.deltaTime;

            // Prevent the player from double jumping by pressing Jump twice and quickly, because
            // the velocity remains at 0 a little while before the AddForce actually occurs.
            if (rb.velocity.y == 0 && jumpDuration >= minJumpDuration)
                isJumping = false;
        }
        // Jump!
        else if (Input.GetButton("Jump"))
            Jump();
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
        isJumping = true;
        jumpDuration = 0f;
    }
}
