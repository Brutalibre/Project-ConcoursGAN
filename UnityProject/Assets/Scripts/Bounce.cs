using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
    public float thrust = 100f;
    public AudioSource sound;
          
    private Transform target;


    void Awake()
    {
        target = transform.FindChild("Target");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Activatable)
        {
            GameObject player = other.transform.parent.gameObject;
            Transform playerTransform = other.transform.parent;

            // Position player on bounce box
            playerTransform.rotation = Quaternion.identity;
            Vector3 newPlayerPosition = playerTransform.position;
            newPlayerPosition.x = transform.position.x;
            playerTransform.position = newPlayerPosition;      

            // Direction to aim for
            Vector3 direction = target.position - playerTransform.position;

            // Add force towards target
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(direction * thrust);

            // Prevent player from moving during bounce time
            Player playerScript = player.GetComponent<Player>();
            if(playerScript != null)
                playerScript.Bounce();

            // Play sound
           // sound.Play();
        }
    }
}
