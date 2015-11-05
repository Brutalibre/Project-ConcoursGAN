using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BluePath : MonoBehaviour 
{
    public float speed = 1f;
    public bool goingForward = true;
    public bool keepMomemtumAtEnd = false;
    public BluePath otherEnd;
    public GameObject blueTrail;

    [HideInInspector]
    public bool waitForExit = false;

    private PlayerControl playerControl = null;
    private CharacterMovement playerMovement = null;
    private bool isMovingPlayer = false;
    private Rigidbody rb;
    private int bluePathIndex = 0;
    private List<Transform> bluePath = new List<Transform>();
    private GameObject blueTrailInstance;

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
            // Script used to move the player
            playerMovement = other.gameObject.GetComponent<CharacterMovement>();

            // Disable user control during while blue path is moving the player
            playerControl = other.gameObject.GetComponent<PlayerControl>();
            playerControl.enabled = false;

            // Put player in the transportation layer
            other.gameObject.layer = LayerMask.NameToLayer(Layers.Ghost);

            // Ignore gravity
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;

            // Hide player in blue path
            //other.gameObject.GetComponent<MeshRenderer>().enabled = false;

            CreateBlueTrail();

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

    void Update()
    {
        if(!isMovingPlayer)
            return;

        UpdateBlueTrail();
    }

    void FixedUpdate()
    {
        if(!isMovingPlayer)
            return;

        // Go towards the next point of the blue path
        Vector3 targetPosition = bluePath[bluePathIndex].position;
        bool targetReached = playerMovement.MoveTowardsTarget(targetPosition, speed);

        // If the target point was reached, move on to the next one
        if (targetReached)
        {
            bluePathIndex++;

            Rigidbody rb = playerControl.gameObject.GetComponent<Rigidbody>();

            // If the last point of the blue path was reached, finish animation
            if(bluePathIndex >= bluePath.Count)
            {
                isMovingPlayer = false;
                bluePathIndex = 0;

                // Put player in their original layer
                playerMovement.gameObject.layer = LayerMask.NameToLayer(Layers.Default);

                // Enable user controls again
                playerControl.enabled = true;

                // Use gravity again
                rb.useGravity = true;

                // Show player again
                Destroy(blueTrailInstance);
                playerMovement.gameObject.GetComponent<MeshRenderer>().enabled = true;

                // Cancel velocity to remove momemtum gathered in the blue path
                if(!keepMomemtumAtEnd)
                    rb.velocity = Vector3.zero;
            }
            // Remove momemtun of blue path object when changing direction
            else
                rb.velocity = Vector3.zero;
        }
    }

    void CreateBlueTrail()
    {
        blueTrailInstance = GameObject.Instantiate(blueTrail) as GameObject;
        blueTrailInstance.transform.SetParent(playerMovement.transform);
        blueTrailInstance.transform.localPosition = Vector3.back;
        blueTrailInstance.transform.localRotation = Quaternion.identity;
    }

    void UpdateBlueTrail()
    {
        Rigidbody rb = playerControl.gameObject.GetComponent<Rigidbody>();
        
        if (rb.velocity.x > 0)
            blueTrailInstance.transform.rotation = Quaternion.LookRotation(Vector3.left);
        else if (rb.velocity.x < 0)
            blueTrailInstance.transform.rotation = Quaternion.LookRotation(Vector3.right);
        else if (rb.velocity.y >= 0)
            blueTrailInstance.transform.rotation = Quaternion.LookRotation(Vector3.down);
        else
            blueTrailInstance.transform.rotation = Quaternion.LookRotation(Vector3.up); 
    }
}
