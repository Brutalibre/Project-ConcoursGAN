using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour 
{
    public float moveSpeed = 4f;
    public Transform start;
    public float waitTimeAtStart = 0f;
    public Transform end;
    public float waitTimeAtEnd = 0f;
    public float waitTimeBeforeStart = 0f;
    public bool waitForPlayer = false;

    private bool towardsEnd = true;
    private float margin = .1f;
    private Rigidbody rb;
    private float waitTime = 0f;
    private bool isStarted = false;

    void Awake()
    {
        transform.position = start.position;
        waitTime = waitTimeAtStart;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Tags.Player)
            waitForPlayer = false;
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

        // Do not move before the time set in the editor
        if (!isStarted)
        {
            CheckStartTime();
            return;
        }

        // Check if platform is paused
        if (waitTime > 0)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        Vector3 targetPos = towardsEnd ? end.position : start.position;
        Vector3 moveVector = targetPos - transform.position;

        // If platform is close enough to target, position it at the target
        if (moveVector.magnitude <= margin)
        {
            transform.position = targetPos;
            rb.velocity = Vector3.zero;
            
            // Pause the platform as set in the editor
            waitTime = towardsEnd ? waitTimeAtEnd : waitTimeAtStart;

            // Go in the other direction
            towardsEnd = !towardsEnd;
        }
        // Otherwise move towards the target
        else
            rb.velocity = moveVector.normalized *  Time.deltaTime * moveSpeed;
    }

    bool CheckStartTime()
    {
        if (waitTimeBeforeStart < 0)
        {
        }
        if (waitTimeBeforeStart < Time.timeSinceLevelLoad)
        {
            rb.isKinematic = false;
            isStarted = true;
            return true;
        }
            
        return false;
    }

    public void Stop()
    {
        isStarted = false;
        waitTime = 0f;
    }
}
