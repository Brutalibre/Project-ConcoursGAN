using UnityEngine;
using System.Collections;

public class RedColorPick : ColorPick 
{
    public GameObject BouncingBoxes;
    public AudioSource sound;

    protected override void OnColorPick()
    {
        // Activate the bouncing boxes
        BouncingBoxes.SetActive(true);

        // Play sound
        sound.Play();
    }
}