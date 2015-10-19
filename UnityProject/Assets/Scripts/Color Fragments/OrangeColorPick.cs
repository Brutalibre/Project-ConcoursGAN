using UnityEngine;
using System.Collections;

public class OrangeColorPick : ColorPick 
{
    private Player playerScript;

    void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
    }

    protected override void OnColorPick()
    {
        // Give the player the ability to jump
        playerScript.EnableJump(true);
    }
}
