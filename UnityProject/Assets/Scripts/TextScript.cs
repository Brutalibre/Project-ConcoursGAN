using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    public float timer = 5;
    string phrase;
    Color color;

    public void launchRoutine(string str, Color col)
    {
        phrase = str;
        color = col;
        StartCoroutine(TextDisplay());
    }

    IEnumerator TextDisplay()
    {
        gameObject.GetComponent<Text>().color = color;
        gameObject.GetComponent<Text>().text = phrase;

        yield return new WaitForSeconds(timer);

        gameObject.GetComponent<Text>().text = "";
        this.enabled = false;
    }
}
