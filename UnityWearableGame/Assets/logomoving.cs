using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logomoving : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 10f;

    private Vector3 startingPosition;
    private bool movingForward = true;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (movingForward)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            if (Vector3.Distance(startingPosition, transform.position) >= distance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
            if (Vector3.Distance(startingPosition, transform.position) <= 0.1f)
            {
                movingForward = true;
            }
        }
    }
}
