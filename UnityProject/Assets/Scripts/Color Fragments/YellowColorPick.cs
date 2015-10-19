using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YellowColorPick : ColorPick 
{
    public List<MovingPlatform> platforms = new List<MovingPlatform>();
    public Material color;

    protected override void OnColorPick()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            // Activate moving platform
            platforms[i].enabled = true;

            // Paint it yellow
            platforms[i].gameObject.GetComponent<MeshRenderer>().material = color;
        }
    }
}
