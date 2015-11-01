using UnityEngine;
using System.Collections;

public class CameraCredits : MonoBehaviour {

    public string[] positions;
    private int[][] pos;
    private int etape;
    public float duration = .5f;
    bool fadeIn = true;
    bool fadeOut = false;
    SceneFadeInOut fadeScr;


    void Awake()
    {
        fadeIn = true;
        fadeScr = gameObject.GetComponent<SceneFadeInOut>();
    }

	// Use this for initialization
	void Start () {

        pos = new int[positions.Length][];
        etape = 0;

        for (int i=0; i<positions.Length; i++)
        {
            pos[i] = new int[3];

            string[] tab = positions[i].Split(' ');

            for(int j=0; j<tab.Length; j++)
            {
                pos[i][j] = int.Parse(tab[j]);
                Debug.Log(i + " "+ j + " " + pos[i][j]);
            }
        }

        transform.position = new Vector3(pos[etape][0], pos[etape][1], transform.position.z);
        transform.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {

        // pos[i][0] = x
        // pos[i][1] = y
        // pos[i][2] = rot(z)
        if (!fadeIn && !fadeOut)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log(transform.rotation);
                etape++;
                StartCoroutine(Transition(new Vector3(pos[etape][0], pos[etape][1], transform.position.z), new Vector3(0, 0, pos[etape][2])));
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                fadeOut = true;
            }
        }

        if (fadeIn)
        {
            if (fadeScr.FadeToClear())
                fadeIn = false;
        }

        if (fadeOut)
        {
            if (fadeScr.FadeToBlack())
            {
                Application.LoadLevel("Main Menu");
            }
        }

        /*transform.position = Vector3.Lerp(transform.position, new Vector3(pos[etape][0], pos[etape][1], transform.position.z), );
        transform.rotation = new Quaternion(0, 0, pos[etape][2], 0);*/


    }

    IEnumerator Transition(Vector3 endPos, Vector3 endRot)
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Quaternion end = Quaternion.identity;
        end.eulerAngles = endRot;

        while (t < duration)
        {
            t += Time.deltaTime * (Time.timeScale / duration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Lerp(startRot, end, t);



            yield return 0;
        }

        
    }
}
