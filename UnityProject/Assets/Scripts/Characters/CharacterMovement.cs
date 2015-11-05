using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    public float moveSpeed = 4f;
    public float moveAcceleration = 100f;
    public float jumpForce = 650f;
    public float stabilisationSpeed = 1f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public bool isGrounded =false;

    private Rigidbody rb;
    private float minDistanceToGround = .05f;
    private float minDistanceToWall = .01f;
    public bool isJumping = false;
    private float stabilisationMargin = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = IsGrounded();
        
        // Update jump status
        if (isJumping && isGrounded)
            isJumping = false;

        // When the player is not grounded anymore or at the edge of the cliff,
        // enable rotation to allow them to fall down
        FreezeRotation(isGrounded || isJumping);

        // When the player is grounded, ensure a stable position
        if (isGrounded)
            StabilizeCharacter();
    }

    public void Move(float horizontalMove)
    {
        // If the the player is grounded and releases the movement button, cancel x velocity
        // to prevent player from sliding forward because of its momentum
        if (horizontalMove == 0)
        {
            if (IsGrounded())
                CancelHorizontalMove();
            return;
        }

        // Ignore move if the player is going towards a wall
        if (!IsFacingWall(horizontalMove))
        {
            float moveForce = moveSpeed * horizontalMove * moveAcceleration;

            // Add horizontal force to the player
            rb.AddForce(new Vector3(moveForce, 0f, 0f));
            
            // Make sure player is not moving too fast
            if (Mathf.Abs(rb.velocity.x) > moveSpeed)
            {
                Vector3 newVelocity = rb.velocity;
                newVelocity.x = newVelocity.x > 0 ? moveSpeed : -moveSpeed;
                
                rb.velocity = newVelocity;
            }
        }
    }

    public bool MoveTowardsTarget(Vector3 targetPosition, float speed)
    {
        Vector3 direction = targetPosition - transform.position;

        float margin = speed < 6 ? .05f : .25f;

        // If this is close enough to the target, set position to that of the target
        if (direction.magnitude < margin)
        {
            transform.position = targetPosition;
            return true;
        }

        rb.velocity = direction.normalized * speed;

        // The target has not been reached yet
        return false;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce);
            isJumping = true;
        }
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    bool IsGrounded()
    {
        float distanceToEdge = transform.localScale.y * .5f;
        bool centerTest = TestRaycast(transform.position, Vector3.down, distanceToEdge);

        // When the player is moving along the ground, use only 1 raycast starting from its center
        if (centerTest)
            return centerTest;

        if (!IsJumping())
            return false;

        // When the player has jumped and is in the air, use 2 more raycasts 
        // in case part of the player lands on an object and should lose balance
        distanceToEdge *= 1.41f;
        bool leftTest = TestRaycast(transform.position, Vector3.right + Vector3.down, distanceToEdge);
        bool rightTest = TestRaycast(transform.position, Vector3.left + Vector3.down, distanceToEdge);

        return (leftTest || rightTest) && rb.velocity.y == 0f;
    }

    bool TestRaycast(Vector3 origin,Vector3 direction, float distanceToEdge)
    {
        RaycastHit hit;
        float minDistance = minDistanceToGround + distanceToEdge;
        float distanceToGround = 0f;
        
        if (Physics.Raycast(origin, direction, out hit, 10.0f, groundLayer)) 
            distanceToGround = hit.distance;
        
        return distanceToGround < minDistance && distanceToGround > 0f;
    }

    bool IsFacingWall(float horizontalMove)
    {
        RaycastHit hit;
        float minDistance = minDistanceToWall + transform.localScale.x * .5f;
        float distanceToWall = 0f;

        Vector3 direction = horizontalMove >= 0 ? Vector3.right : Vector3.left;
        if (Physics.Raycast(transform.position, direction, out hit, 10.0f, wallLayer)) 
            distanceToWall = hit.distance;

        return distanceToWall < minDistance && distanceToWall > 0f;
    }

    void CancelHorizontalMove()
    {
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = 0f;
        rb.velocity = newVelocity;
    }

    void FreezeRotation(bool freeze)
    {
        if (freeze)
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        else
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    void StabilizeCharacter()
    {
        float lerp = Mathf.Lerp(transform.localRotation.z, 0, Time.deltaTime * stabilisationSpeed);
        lerp = transform.localRotation.z - lerp;

        if (Mathf.Abs(lerp) <= stabilisationMargin)
            transform.rotation = Quaternion.identity;
        else
            transform.Rotate(Vector3.forward, lerp);
    }
}
