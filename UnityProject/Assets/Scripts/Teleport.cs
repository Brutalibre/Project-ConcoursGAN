using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public bool inDoor = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            inDoor = true;
            GetComponentInParent<InsidePortal>().GoToOtherDoor(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            inDoor = false;
        }
    }
}
