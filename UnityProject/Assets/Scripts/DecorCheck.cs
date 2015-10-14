using UnityEngine;
using System.Collections;

public class DecorCheck : MonoBehaviour 
{
    public bool againstDecor = false;

    void OnTriggerEnter()
    {
        againstDecor = true;
    }

    void OnTriggerStay()
    {
        againstDecor = true;
    }

    void OnTriggerExit()
    {
        againstDecor = false;
    }
}
