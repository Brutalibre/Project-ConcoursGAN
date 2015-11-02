using UnityEngine;
using System.Collections;

public class StartGameAnimation : MonoBehaviour 
{
    public Transform target;
    public float animationSpeed = 2f;

    public bool isStarted = false;
    private bool isFinished =  false;
    private float margin = .5f;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = target.position;
    }

    public void StartAnimation()
    {
        isStarted = true;
    }

    public bool IsFinished()
    {
        return isFinished;
    }

    void Update()
    {
        if (!isStarted)
            return;

        if (isFinished)
            return;

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, animationSpeed * Time.deltaTime);

        Vector3 distanceVector = targetPosition - newPosition;
        if (distanceVector.magnitude <= margin)
        {
            newPosition = targetPosition;
            isFinished = true;
        }

        transform.position = newPosition;
    }
}
