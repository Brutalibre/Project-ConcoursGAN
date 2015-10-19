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
    public float colorBurstForce = 100f;
    public float colorBurstRadius = 3f;
    public float colorBurstDistanceToCenter = .15f;
    public GameObject colorCube;
    
    private List<ColorCollector.Color> collectedColors = new List<ColorCollector.Color>();
    private List<Vector3> burstPositions = new List<Vector3>();
    private List<Rigidbody> waitingForBurst = new List<Rigidbody>();
    private Vector3 burstPosition;

    void Start()
    {
        // The player starts with the gray color only
        Collect(Color.Gray);

        // When the cube bursts, all colors are placed around the center of the player
        // The following list enumerate all positions for the colorCubes
        // It allows us to randomly assign positions to color (the explosion's color dispersion will vary)
        burstPositions.Add(new Vector3(0f,-randomizedBurstDistance(),0f));
        burstPositions.Add(new Vector3(randomizedBurstDistance(),0f,0f));
        burstPositions.Add(new Vector3(0f,randomizedBurstDistance(),0f));
        burstPositions.Add(new Vector3(-randomizedBurstDistance(),0f,0f));
        burstPositions.Add(new Vector3(randomizedBurstDistance(),-randomizedBurstDistance(),0f));
        burstPositions.Add(new Vector3(randomizedBurstDistance(),randomizedBurstDistance(),0f));
        burstPositions.Add(new Vector3(-randomizedBurstDistance(),randomizedBurstDistance(),0f));
        burstPositions.Add(new Vector3(-randomizedBurstDistance(),-randomizedBurstDistance(),0f));
    }

    void FixedUpdate()
    {
        // Create burst animation if BurstColors() was called 
        if (waitingForBurst.Count > 0)
        {
            for(int i = 0; i < waitingForBurst.Count; i++)
                waitingForBurst[i].AddExplosionForce(colorBurstForce,burstPosition,colorBurstRadius);
               
            waitingForBurst.Clear();
        }
    }

    public void Collect(Color color)
    {
        if (!collectedColors.Contains(color))
            collectedColors.Add(color);
    }
    
    public void BurstColors(Vector3 targetPosition)
    {
        // Create a colored cube for each collected color 
        // This animation is triggered when the player dies

        int collectedColorsCount = collectedColors.Count;
        for (int i = 0; i < collectedColorsCount; i++)
        {
            // Get random position for the current color
            int randomIndex = Random.Range(0,collectedColors.Count);
            Vector3 position = burstPositions[randomIndex];
            position += targetPosition;
            burstPosition = targetPosition;
            burstPositions.RemoveAt(randomIndex);
            
            // Instantiate cube and give it the current color
            GameObject instance = Instantiate(colorCube,position,Quaternion.identity) as GameObject;
            instance.GetComponent<MeshRenderer>().material = materialFromColor(collectedColors[0]);
            waitingForBurst.Add(instance.GetComponent<Rigidbody>());

            // Remove current color from collection
            collectedColors.RemoveAt(0);
        }
    }

    Material materialFromColor(Color color)
    {
        switch (color)
        {
            case Color.Gray:
                return gray;
            case Color.Blue:
                return blue;
            case Color.Green:
                return green;
            case Color.Indigo:
                return indigo;
            case Color.Orange:
                return orange;
            case Color.Red:
                return red;
            case Color.Violet:
                return violet;
            case Color.Yellow:
                return yellow;
            default:
                break;
        }

        return gray;
    }

    float randomizedBurstDistance()
    {
        return colorBurstDistanceToCenter + Random.Range(-.1f,.1f);
    }
}
