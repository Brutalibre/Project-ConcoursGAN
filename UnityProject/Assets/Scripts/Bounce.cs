using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
    public float thrust = 100f;
          
    private Transform target;

    void Awake()
    {
        target = transform.FindChild("Target");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            // Player transform
            Transform player = other.transform;

            // Direction to aim for
            Vector3 direction = target.position - player.position;

            // Add force towards target
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(direction * thrust);

            // Enable rotation for better visual impact
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            // Prevent player from moving during bounce time
            other.GetComponent<Player>().Bounce();
        }
    }
}
