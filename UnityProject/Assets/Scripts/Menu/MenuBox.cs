using UnityEngine;
using System.Collections;

public class MenuBox : MonoBehaviour {

    public float spinSpeed;
    public CursorScript script;

    // Use this for initialization
    void Start () {
        // mat[0] is indigo
        gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, Vector3.up, spinSpeed);
        transform.RotateAround(transform.position, Vector3.forward, spinSpeed);
        transform.RotateAround(transform.position, Vector3.right, spinSpeed);

        gameObject.GetComponent<MeshRenderer>().material.color = script.col;
    }
}
