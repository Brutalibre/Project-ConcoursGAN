using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BluePath : MonoBehaviour 
{
    public float speed = 1f;
    public bool goingForward = true;
    public BluePath otherEnd;
    [HideInInspector]
    public bool waitForExit = false;


    private Player playerScript = null;
    private Transform player = null;
    private bool isMovingPlayer = false;
    private Rigidbody rb;
    private int bluePathIndex = 0;
    private List<Transform> bluePath = new List<Transform>();

    void Start()
    {
        // Add start point to the blue path
        bluePath.Insert(0,transform);

        // Add all other points
        Transform keyFrames = transform.parent.FindChild("KeyFrames");

        for (int i = 0; i < keyFrames.childCount; i++)
        {
            int index = goingForward ? i : keyFrames.childCount - 1 - i;
            bluePath.Add(keyFrames.GetChild(index));
        }

        // Add end
        bluePath.Add(otherEnd.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (waitForExit)
            return;

        if(other.tag == Tags.Player)
        {
            player = other.transform;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            playerScript = other.gameObject.GetComponent<Player>();
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            playerScript.enabled = false;
            isMovingPlayer = true;

            otherEnd.waitForExit = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (waitForExit)
        {
            if(other.tag == Tags.Player)
                waitForExit = false;
        }
    }

    void FixedUpdate()
    {
        if(!isMovingPlayer)
            return;

        // Go towards the next point of the blue path
        Vector3 target = bluePath[bluePathIndex].position;Debug.Log(target);
        Vector3 direction = target - player.position;
        Vector3 move = direction.normalized * speed;
        Vector3 newPlayerPosition = player.position + move;

        // Make sure player doesn't go beyond the blue path points

        // If the player is going to the right or left, make sure they don't go beyond the current point
        int horizontalDirection = direction.x >= 0 ? 1 : -1;
        if(newPlayerPosition.x * horizontalDirection > bluePath[bluePathIndex].position.x * horizontalDirection)
            newPlayerPosition.x = bluePath[bluePathIndex].position.x;

        // If the player is going up or down, make sure they don't go beyond the current point
        int verticalDirection = direction.y >= 0 ? 1 : -1;
        if (newPlayerPosition.y * verticalDirection > bluePath[bluePathIndex].position.y * verticalDirection)
            newPlayerPosition.y = bluePath[bluePathIndex].position.y;

        // Update player position
        player.position = newPlayerPosition;

        // If the target point was reached, move on to the next one
        if (player.position.x == target.x && player.position.y == target.y)
        {
            player.position = target;
            bluePathIndex++;

            // If the last point of the blue path was reached, finish animation
            if(bluePathIndex >= bluePath.Count)
            {
                isMovingPlayer = false;
                bluePathIndex = 0;
                playerScript.enabled = true;
                rb.isKinematic = false;
                player.gameObject.GetComponent<BoxCollider>().enabled = true;
                playerScript.ResetDecorCheck();
            }
        }
    }
}
