using UnityEngine;
using System.Collections;
using System;

public class BlueColorPick : ColorPick {

    public GameObject SecretPaths;

    protected override void OnColorPick()
    {
        // Activate the bouncing boxes
        SecretPaths.SetActive(true);
    }
}
