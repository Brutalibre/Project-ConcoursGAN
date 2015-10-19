using UnityEngine;
using System.Collections;

public class BouncingBoxPlacementHelper : MonoBehaviour 
{
    public DecorCheck groundCheck;
    public float repositioningTime = .5f;

    private Vector3 originalPosition;
    private bool isRepositioning = false;
    private float elapsedTime = 0f;
    private float waitTime = .5f;

    void Awake()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isRepositioning)
            return;

        // Wait a short time for the test cube to bounce off the red box
        elapsedTime += Time.deltaTime;
        if (elapsedTime < waitTime)
            return;

        // If test block has reached the destination (i.e. is grounded again), send it back to the original position
        // so that the user can ajdust the target in real time in the editor
        if (groundCheck.againstDecor)
        {
            isRepositioning = true;
            elapsedTime = 0f;

            StartCoroutine(Reposition());
        }
    }

    IEnumerator Reposition()
    {
        yield return new WaitForSeconds(repositioningTime);

        transform.position = originalPosition;
        isRepositioning = false;
    }
}
