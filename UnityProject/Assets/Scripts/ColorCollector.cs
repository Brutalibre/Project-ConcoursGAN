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
    private Vector3 burstPosition;
    private bool burstColors = false;
    private List<GameObject> colorCubes = new List<GameObject>();

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
        if (burstColors)
        {
            for(int i = 0; i < colorCubes.Count; i++)
                colorCubes[i].GetComponent<Rigidbody>().AddExplosionForce(colorBurstForce,burstPosition,colorBurstRadius);

            burstColors = false;
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

        List<ColorCollector.Color> colors = new List<ColorCollector.Color>(collectedColors);
        List<Vector3> positions = new List<Vector3>(burstPositions);
        int collectedColorsCount = colors.Count;
        for (int i = 0; i < collectedColorsCount; i++)
        {
            // Get random position for the current color
            int randomIndex = Random.Range(0,colors.Count);
            Vector3 position = positions[randomIndex];
            position += targetPosition;
            burstPosition = targetPosition;
            positions.RemoveAt(randomIndex);
            
            // Instantiate cube and give it the current color
            GameObject instance = Instantiate(colorCube,position,Quaternion.identity) as GameObject;
            instance.GetComponent<MeshRenderer>().material = materialFromColor(colors[0]);
            colorCubes.Add(instance);

            // Remove current color from collection
            colors.RemoveAt(0);
        }

        burstColors = true;
    }

    public void CleanUp()
    {
        int count = colorCubes.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(colorCubes[i]);
        }

        colorCubes.Clear();
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
