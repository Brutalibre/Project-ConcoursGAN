using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour {

    public AudioClip firstClip;
    public AudioClip loop;
	

    void Start()
    {
        GetComponent<AudioSource>().loop = true;
        StartCoroutine(playEngineSound());
    }
    
    IEnumerator playEngineSound()
    {
        GetComponent<AudioSource>().clip = firstClip;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        GetComponent<AudioSource>().clip = loop;
        GetComponent<AudioSource>().Play();

    }
}
