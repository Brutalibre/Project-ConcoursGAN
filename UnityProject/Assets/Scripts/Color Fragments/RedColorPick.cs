using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedColorPick : ColorPick 
{
    public GameObject BouncingBoxes;
    public Text redText;

    protected override void OnColorPick()
    {
        // Activate the bouncing boxes
        BouncingBoxes.SetActive(true);

        // Add color to the collection
        colorCollector.Collect(ColorCollector.Color.Red);

        // Activate help text
        redText.GetComponent<TextScript>().enabled = true;
        redText.GetComponent<TextScript>().launchRoutine("Utilisez les blocs rouges pour rebondir!", Color.red);

    }
}