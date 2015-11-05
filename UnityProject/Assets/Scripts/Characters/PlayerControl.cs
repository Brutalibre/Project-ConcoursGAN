using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
    private CharacterMovement moveScript;

    private float horizontalMove;
    private bool jump = false;

    void Awake()
    {
        moveScript = GetComponent<CharacterMovement>();
    }

    void Update()
    {    
        // Read direction input from player
        // The sensitivity should be lower on ground to allow precise movement near edges;
        // and higher in air to allow better reactivity 
        if (moveScript.IsJumping())
            horizontalMove = Input.GetAxis("AirHorizontal");
        else 
            horizontalMove = Input.GetAxis("Horizontal");
        
        // Check jump
        if (Input.GetButtonDown("Jump"))
            jump = true;
    }


    void FixedUpdate()
    { 
        // Move player 
        moveScript.Move(horizontalMove);

        // Check jump
        if (jump)
        {
            moveScript.Jump();
            jump = false;
        }
    }
}
