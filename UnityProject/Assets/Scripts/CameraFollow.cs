using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float followSpeed = 4f;

	private Transform player;
	private float margin = 0.02f;

	void Start() 
	{
        // Reference to player's transform component
		player = GameObject.FindGameObjectWithTag (Tags.Player).transform;

        // Set position to that of the player
        Vector3 startPosition = player.position;
        startPosition.z = transform.position.z;
        transform.position = startPosition;
	}

	void FixedUpdate() 
	{
        Vector3 newPosition;
        Vector2 projection = player.position - transform.position;
        
        // If camera is close enough to the player, set the same position
		if (projection.magnitude <= margin)
        {
            newPosition = player.position;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        } 
        // Otherwise follow the player smoothly
        else
        {
            Vector3 position = transform.position;
            position.z = player.position.z;

            newPosition = Vector3.Lerp (position, player.position, followSpeed);
            newPosition.z = transform.position.z;
        }

        // Update camera position
		transform.position = newPosition;
	}
}
