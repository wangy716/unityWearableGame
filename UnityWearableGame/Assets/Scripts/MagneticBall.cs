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
                Rigidbody ballRb = ball.GetComponent<Rigidbody>();
                if (ballRb != null)  // Check if the Rigidbody component is attached
                {
                    Vector3 direction = ball.transform.position - transform.position;
                    float distance = direction.magnitude;
                    Vector3 force = direction.normalized * magneticForce / (distance * distance);  // Use inverse square law for magnetic force
                    ballRb.AddForce(-force);
                }
            }
        }
    }
}
