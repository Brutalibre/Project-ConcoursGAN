using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GreenColorPick : ColorPick {

    public List<GameObject> greenPlatforms = new List<GameObject>();
    public Material color;

    protected override void OnColorPick()
    {
        for (int i = 0; i < greenPlatforms.Count; i++)
        {
            // get all GameObjects
            GameObject platf = greenPlatforms[i].transform.Find("Platform").gameObject;
            GameObject sup = greenPlatforms[i].transform.Find("Support").gameObject;
            GameObject rope = greenPlatforms[i].transform.Find("Rope").gameObject;

            // Activate support + rope

            sup.SetActive(true);
            rope.SetActive(true);

            // Make isKinematic=false and useGravity = true for Platform
            platf.GetComponent<Rigidbody>().useGravity = true;
            platf.GetComponent<Rigidbody>().isKinematic = false;

            // Paint all the shit green
            sup.gameObject.GetComponent<MeshRenderer>().material = color;
            rope.gameObject.GetComponent<MeshRenderer>().material = color;

        }
    }
}
