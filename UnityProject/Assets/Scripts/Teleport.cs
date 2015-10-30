using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{

    public bool inDoor = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.Player)
        {
            Debug.Log("player enter");
            inDoor = true;
            GetComponentInParent<InsidePortal>().GoToOtherDoor(GetComponent<GameObject>());
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
