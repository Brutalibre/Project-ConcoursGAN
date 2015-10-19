using UnityEngine;
using System.Collections;

public abstract class ColorPick : MonoBehaviour 
{
    public float spinSpeed = 3f;
    public ColorCollector colorCollector;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            OnColorPick();
            
            // Add color to the collection
            colorCollector.Collect(ColorCollector.Color.Red);
            
            // Hide this object
            gameObject.SetActive(false);
        }
    }

    protected void FixedUpdate()
    {
        // Give a rotation effect to this object for better visual
        transform.RotateAround(transform.position, Vector3.up, spinSpeed);
    }

    protected abstract void OnColorPick();
}
