using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorCollector : MonoBehaviour 
{
    public enum Color
    {
        Gray,Red,Orange,Yellow,Green,Blue,Indigo,Violet
    }

    public Material gray;
    public Material red;
    public Material orange;
    public Material yellow;
    public Material green;
    public Material blue;
    public Material indigo;
    public Material violet;
    public float colorBurstPower = 100f;
    public float colorBurstRange = 3f;
    
    private List<ColorCollector.Color> collectedColors = new List<ColorCollector.Color>();
    
    public void Collect(Color color)
    {
        if (!collectedColors.Contains(color))
            collectedColors.Add(color);
    }
    
    public void BurstColors()
    {
        Debug.Log("Burst");   
    }
}
