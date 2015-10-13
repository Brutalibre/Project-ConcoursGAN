using UnityEngine;
using System.Collections;

public class OrangeColorPick : MonoBehaviour 
{
    public float spinSpeed = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            // Give the player the ability to jump
            other.gameObject.GetComponent<Player>().EnableJump(true);

            // Hide this object
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        // Give a rotation effect to this object for better visual
        transform.RotateAround(transform.position, Vector3.up, spinSpeed);
    }
}
