using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class VioletColorPick : ColorPick {

    public GameObject boulders;
    public GameObject platforms;
    public Text txt;

    protected override void OnColorPick()
    {
        boulders.SetActive(true);
        platforms.SetActive(true);

        // Activate help text
        txt.GetComponent<TextScript>().enabled = true;
        txt.GetComponent<TextScript>().launchRoutine("Entrez dans un portail violet pour être téléporté de l'autre côté!", new Color(0.545f,0.294f,0.698f));

    }
}
