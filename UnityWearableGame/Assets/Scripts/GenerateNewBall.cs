using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewBall : MonoBehaviour
{
    [Header("Ball Prefab")]
    [SerializeField] private GameObject ballPrefab;

    private float initZ;
    void Start()
    {
        if(transform.childCount == 0)
        {
            Instantiate(ballPrefab, transform);
        }

        initZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            
            float zMultiply = 1;
            foreach (Transform child in transform)
            {
                zMultiply *= (child.transform.position.z - initZ);              
            }
            if (zMultiply > 0)
            {
                Instantiate(ballPrefab, transform);
            }
            
        }
    }
}
