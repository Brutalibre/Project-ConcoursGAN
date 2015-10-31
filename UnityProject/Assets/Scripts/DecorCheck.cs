using UnityEngine;
using System.Collections;

public class DecorCheck : MonoBehaviour 
{
    public bool againstDecor = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != Tags.Portal)
            againstDecor = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != Tags.Portal)
            againstDecor = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != Tags.Portal)
            againstDecor = false;
    }
}
