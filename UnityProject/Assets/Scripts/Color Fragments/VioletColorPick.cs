using UnityEngine;
using System.Collections;
using System;

public class VioletColorPick : ColorPick {

    public GameObject boulders;
    public GameObject platforms;

    protected override void OnColorPick()
    {
        boulders.SetActive(true);
        platforms.SetActive(true);
    }
}
