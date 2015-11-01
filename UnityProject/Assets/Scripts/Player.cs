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
    public DecorCheck leftDecorCheck;
    public DecorCheck rightDecorCheck;
    public DecorCheck groundCheck;
    public bool canJump = false;

    private Rigidbody rb;
    private bool isBouncing = false;
    private float bounceDuration = 0f;
    private float minBounceDuration = 0.1f;
    private bool isMinBoucing = false;
    public bool canMove = true;
    private bool isJumping = false;
    private float minJumpDuration = 0.1f;
    private float jumpDuration = 0f;

    public bool menuOpen = false;
    public Canvas menu;

    public AudioSource deathSound;

    private bool fadeIn = true;
    private bool fadeOut = false;
    public SceneFadeInOut fadeScr;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        menu.enabled = false;
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
        if (!fadeIn && !fadeOut && !menuOpen)
        { 
            // Move the player according to the user input
            Move();
        }

        /*if (menu.enabled)
        {
            canMove = false; 
            canJump = false;
            menuOpen = true;

            // appuyer sur Echap lorsque le menu est ouvert le ferme.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuOpen = false;

                Debug.Log("close");

                menu.enabled = false;
            }

        }


        else if (!menu.enabled)
        {
            canMove = true;
            canJump = true;
            menuOpen = false;

            // appuyer sur Echap lorsque le menu est fermé l'ouvre.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuOpen = true;
                Debug.Log("open");
                menu.enabled = true;
            }

        }*/



        if (fadeIn)
        {
            if (fadeScr.FadeToClear())
                fadeIn = false;
        }

        if (fadeOut)
        {
            if (fadeScr.FadeToBlack())
            {
                Application.LoadLevel("Main Menu");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Enemy)
        {
            Die();
        }
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

        // Cancel move if player is against decor
        bool facingRight = horizontalMove > 0 ? true : false;
        if (IsAgainstDecor(facingRight))
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
    
    void Die()
    {
        // The scene will be reloaded by the game controller
        GameController.instance.PlayerDied();

        // Death animation
        colorCollector.BurstColors(transform.position);

        deathSound.Play();

        rb.velocity = Vector3.zero;

        // Remove player from the scene
        gameObject.SetActive(false);
    }

    public void Respawn(Vector3 position)
    {
        transform.position = position;
        ResetDecorCheck();
        gameObject.SetActive(true);
    }

    public void ResetDecorCheck()
    {
        // Reset decor check
        groundCheck.againstDecor = false;
        rightDecorCheck.againstDecor = false;
        leftDecorCheck.againstDecor = false;
    }

    bool IsAgainstDecor(bool facingRight)
    {
        return facingRight ? rightDecorCheck.againstDecor : leftDecorCheck.againstDecor;
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
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
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
            if (jumpDuration >= minJumpDuration && groundCheck.againstDecor)
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

        gameObject.GetComponentInChildren<AudioSource>().Play();
    }
}
