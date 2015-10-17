using UnityEngine;
using System.Collections;

public class RedColorPick : MonoBehaviour 
{
    public float spinSpeed = 6f;
    public ColorCollector colorCollector;
    public GameObject BouncingBoxes;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            // Give the player the ability to jump
            BouncingBoxes.SetActive(true);

            // Add color to the collection
            colorCollector.Collect(ColorCollector.Color.Red);

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
