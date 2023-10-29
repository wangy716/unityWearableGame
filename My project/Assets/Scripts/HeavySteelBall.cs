using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySteelBall : BallMovement
{
    [Header("Heavy Properties")]
    [SerializeField] private float mass = 5f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        rb.mass = mass;
    }
}
