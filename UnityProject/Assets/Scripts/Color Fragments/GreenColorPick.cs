using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GreenColorPick : ColorPick {

    public List<GameObject> greenPlatforms = new List<GameObject>();
    public Material color;
    public Text txt;

    protected override void OnColorPick()
    {
        for (int i = 0; i < greenPlatforms.Count; i++)
        {
            // get all GameObjects
            GameObject platf = greenPlatforms[i].transform.Find("Platform").gameObject;
            GameObject sup = greenPlatforms[i].transform.Find("Support").gameObject;
            GameObject rope = greenPlatforms[i].transform.Find("Rope").gameObject;

            colorCollector.Collect(ColorCollector.Color.Green);

            // Activate support + rope

            sup.SetActive(true);
            rope.SetActive(true);

            // Make isKinematic=false and useGravity = true for Platform
            platf.GetComponent<Rigidbody>().useGravity = true;
            platf.GetComponent<Rigidbody>().isKinematic = false;

            // Paint all the shit green
            sup.gameObject.GetComponent<MeshRenderer>().material = color;
            rope.gameObject.GetComponent<MeshRenderer>().material = color;

            // Activate help text
            txt.GetComponent<TextScript>().enabled = true;
            txt.GetComponent<TextScript>().launchRoutine("Courez sur les plateformes suspendues pour les faire balancer!", Color.green);


        }
    }
}
