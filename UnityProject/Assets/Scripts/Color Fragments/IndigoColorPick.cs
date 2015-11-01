using UnityEngine;
using System.Collections;
using System;

public class IndigoColorPick : ColorPick {
    
    protected override void OnColorPick()
    {
        Application.LoadLevel("Credits");
    }

}
