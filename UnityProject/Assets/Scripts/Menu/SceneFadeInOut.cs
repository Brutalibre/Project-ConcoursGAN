using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 3f;
    

    public bool FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);

        Debug.Log("fadeClear");

        // when the color is almost clear, return true
        if (FadeImg.color.a <= 0.05f)
            return true;

        else return false;
    }


    public bool FadeToBlack()
    {
        FadeImg.enabled = true;

        Debug.Log("fadeBlack");

        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);

        // when the color is almost black, return true
        if (FadeImg.color.a >= 0.95f)
            return true;

        else return false;
    }
}