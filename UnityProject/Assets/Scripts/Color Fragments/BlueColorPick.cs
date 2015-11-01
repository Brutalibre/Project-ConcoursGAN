using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BlueColorPick : ColorPick {

    public GameObject SecretPaths;
    public Text txt;

    protected override void OnColorPick()
    {
        // Activate the bouncing boxes
        SecretPaths.SetActive(true);

        // Activate help text
        txt.GetComponent<TextScript>().enabled = true;
        txt.GetComponent<TextScript>().launchRoutine("Marchez sur les blocs bleus pour prendre des tunnels!", Color.blue);

    }
}
