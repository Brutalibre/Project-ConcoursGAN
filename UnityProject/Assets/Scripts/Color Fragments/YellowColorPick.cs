using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class YellowColorPick : ColorPick 
{
    public List<MovingPlatform> platforms = new List<MovingPlatform>();
    public Material color;
    public Text txt;

    private List<Vector3> originalPositions = new List<Vector3>();
    private List<bool> waitForPlayer = new List<bool>();

    void Start()
    {
        // Save the original position of all platforms 
        // Used to reset platforms upon player's death
        for(int i = 0; i < platforms.Count; i++)
        {
            originalPositions.Add(platforms[i].transform.position);
            waitForPlayer.Add(platforms[i].waitForPlayer);
        }
    }

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
            txt.GetComponent<TextScript>().launchRoutine("Touchez les plateformes jaunes pour les activer!", Color.yellow);

        }
    }

    public override void Reset()
    {
        for(int i = 0; i < platforms.Count; i++)
        {
            platforms[i].Stop();
            platforms[i].transform.position = originalPositions[i];
            platforms[i].waitForPlayer = waitForPlayer[i];
        }
    }
}
