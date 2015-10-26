using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransformAnimation : MonoBehaviour 
{
    public float moveSpeed = 150f;
    public float waitTimeAtStart = 0f;
    public float waitTimeAtEnd = 0f;
    public bool waitForPlayer = false;
    public List<Transform> keyFrames = new List<Transform>();
    
    private bool towardsEnd = true;
    private float margin = .1f;
    private Rigidbody rb;
    private float waitTime = 0f;
    private bool isStarted = false;
    private int currentIndex = 0;
    private Transform player;
    bool playerOn = false;

    void Awake()
    {
        waitTime = waitTimeAtStart;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Tags.Player)
        {
            waitForPlayer = false;
            player = other.transform;
        }
    }
    
    void Update()
    {
        // Update wait time 
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            if(waitTime < 0)
                waitTime = 0f;
        }
    }
    
    void FixedUpdate()
    {
        // Do not move is the platform should wait for the player
        if (waitForPlayer)
            return;
        
        // Check if platform is paused
        if (waitTime > 0)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        Vector3 targetPos = keyFrames[currentIndex].position;
        Vector3 moveVector = targetPos - transform.position;
        
        // If platform is close enough to target, position it at the target
        if (moveVector.magnitude <= margin)
        {
            transform.position = targetPos;
            rb.velocity = Vector3.zero;
            
            // Pause the platform as set in the editor
            waitTime = towardsEnd ? waitTimeAtEnd : waitTimeAtStart;

            // Update target
            currentIndex += towardsEnd ? 1 : -1;

            if(towardsEnd && currentIndex >= keyFrames.Count)
            {
                currentIndex = keyFrames.Count - 1;
                towardsEnd = false;
            }
            else if(!towardsEnd && currentIndex < 0)
            {
                currentIndex = 0;
                towardsEnd = true;
            }
        }
        // Otherwise move towards the target
        else
            rb.velocity = moveVector.normalized *  Time.deltaTime * moveSpeed;
    }
}
