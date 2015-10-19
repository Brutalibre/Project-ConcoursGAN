using UnityEngine;
using System.Collections;

public class RedColorPick : ColorPick 
{
    public GameObject BouncingBoxes;

    protected override void OnColorPick()
    {
        // Activate the bouncing boxes
        BouncingBoxes.SetActive(true);
    }
}
