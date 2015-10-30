using UnityEngine;
using System.Collections;

public class InsidePortal : MonoBehaviour {

    public bool insidePortal = false;
    public Player player;
    public GameObject entrance;
    public GameObject exit;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        CheckDoors();
	}

    public void GoToOtherDoor(GameObject departure)
    {
        if (!insidePortal)
        {
            if (departure == entrance)
                player.transform.position = entrance.transform.position;
            else
                player.transform.position = exit.transform.position;

            insidePortal = true;
        }
    }

    public void CheckDoors()
    {
        if (entrance.GetComponent<Teleport>().inDoor || exit.GetComponent<Teleport>().inDoor)
        {
            insidePortal = true;
        }

        else insidePortal = false;
    }
}
