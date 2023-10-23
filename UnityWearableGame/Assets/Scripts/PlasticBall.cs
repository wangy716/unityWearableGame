using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBall : BallMovement
{
    [Header("Plastic Properties")]
    [SerializeField] private float mass = 2f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        rb.mass = mass;
    }
}
