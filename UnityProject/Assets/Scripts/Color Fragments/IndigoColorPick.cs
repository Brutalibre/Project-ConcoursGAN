﻿using UnityEngine;
using System.Collections;
using System;

public class IndigoColorPick : ColorPick {
    
    protected override void OnColorPick()
    {

        colorCollector.Collect(ColorCollector.Color.Indigo);
        Application.LoadLevel("Credits");
    }

}
