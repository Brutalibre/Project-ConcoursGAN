using UnityEngine;
using System.Collections;

public class WhiteColorPick : ColorPick 
{
    protected override void OnColorPick()
    {
        GameController.instance.SetCheckPoint(transform.position);

        Destroy(transform.parent.gameObject);
    }
}
