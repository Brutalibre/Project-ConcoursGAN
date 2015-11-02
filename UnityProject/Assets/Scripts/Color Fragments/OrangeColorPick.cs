using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class OrangeColorPick : ColorPick 
{
    private Player playerScript;
    public Text orangeText;

    void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
    }

    protected override void OnColorPick()
    {
        // Give the player the ability to jump
        playerScript.EnableJump(true);


        colorCollector.Collect(ColorCollector.Color.Orange);

        // Activate help text
        orangeText.GetComponent<TextScript>().enabled = true;
        orangeText.GetComponent<TextScript>().launchRoutine("Appuyez sur ESPACE pour sauter!", new Color(1, 0.474f, 0));
    }
}
