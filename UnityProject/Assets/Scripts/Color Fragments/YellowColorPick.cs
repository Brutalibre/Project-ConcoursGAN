using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class YellowColorPick : ColorPick 
{
    public List<MovingPlatform> platforms = new List<MovingPlatform>();
    public Material color;
    public Text txt;


    protected override void OnColorPick()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            // Activate moving platform
            platforms[i].enabled = true;

            // Paint it yellow
            platforms[i].gameObject.GetComponent<MeshRenderer>().material = color;

            // Activate help text
            txt.GetComponent<TextScript>().enabled = true;
            txt.GetComponent<TextScript>().launchRoutine("Touchez les plateformes jaunes pour les activer !", Color.yellow);

        }
    }
}
