using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

    public float spinSpeed;
    private int[] values = new int[3];
    private int pos;

	// Use this for initialization
	void Start () {
        values[0] = 6;
        values[1] = 1;
        values[2] = -4;

        pos = 0;

        // Le curseur est sur Start Game au début.
        transform.position = new Vector3(transform.position.x, values[pos], transform.position.z);
    }

    // Update is called once per frame
    void Update () {
        // Give a rotation effect to this object for better visual
        transform.RotateAround(transform.position, Vector3.up, spinSpeed);
        transform.RotateAround(transform.position, Vector3.forward, spinSpeed);
        transform.RotateAround(transform.position, Vector3.right, spinSpeed);

        


        Debug.Log(pos);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (pos < 2)
                pos++;

            else
                pos = 0;

            transform.position = new Vector3(transform.position.x, values[pos], transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (pos > 0)
                pos--;

            else
                pos = 2;

            transform.position =  new Vector3(transform.position.x, values[pos], transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (pos)
            {
                // Start Game
                case 0:
                    Application.LoadLevel("First_Level");
                    break;

                // Credits
                case 1:
                    //Application.LoadLevel("Credits");
                    break;
                
                // Exit
                case 2:
                    Application.Quit();
                    break;
            }
        }
    }
}
