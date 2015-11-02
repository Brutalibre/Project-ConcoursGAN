using UnityEngine;
using System.Collections;

public class InsidePortal : MonoBehaviour {

    public bool insidePortal = false;
    public Player player;
    public GameObject entrance;
    public GameObject exit;
	
	// Update is called once per frame
	void Update () {
        CheckDoors();
	}

    public void GoToOtherDoor(GameObject departure)
    {
        if (!insidePortal)
        {
            if (departure == entrance)
            {
                player.transform.position = exit.transform.position;
                Debug.Log("entrance");
            }
            else
            {
                player.transform.position = entrance.transform.position;
                Debug.Log("exit");
            }

            player.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameController.instance.FreezePlayer(.3f);

            gameObject.GetComponentInChildren<AudioSource>().Play();
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
