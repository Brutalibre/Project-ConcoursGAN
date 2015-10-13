using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour 
{
    public float moveSpeed = 4f;
    public Transform start;
    public Transform end;

    private bool towardsEnd = true;
    private float margin = .1f;

    void Awake()
    {
        transform.position = start.position;
    }

    void Update()
    {
        Vector3 targetPos = towardsEnd ? end.position : start.position;
        Vector3 moveVector = targetPos - transform.position;

        if (moveVector.magnitude <= margin)
        {
            transform.position = targetPos;
            towardsEnd = !towardsEnd;
        }
        else
            transform.position += moveVector.normalized *  Time.deltaTime * moveSpeed;
    }
}
