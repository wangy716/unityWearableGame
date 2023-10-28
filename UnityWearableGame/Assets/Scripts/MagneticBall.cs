using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBall : BallMovement
{
    [Header("Magnetic Properties")]
    [SerializeField] private float magneticForce = 10f;

    private void FixedUpdate()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            if (ball != gameObject && ball.tag != "PlasticBall")  // Check if the ball is not a plastic ball
            {
                Vector3 direction = ball.transform.position - transform.position;
                float distance = direction.magnitude;
                Vector3 force = direction.normalized * magneticForce / distance;
                ball.GetComponent<Rigidbody>().AddForce(-force);
            }
        }
    }
}
