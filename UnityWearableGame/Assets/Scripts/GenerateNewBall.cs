using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewBall : MonoBehaviour
{
    [Header("Ball Prefabs")]
    [SerializeField] private GameObject magneticBallPrefab;
    [SerializeField] private GameObject heavyBallPrefab;
    [SerializeField] private GameObject plasticBallPrefab;

    private GameObject selectedBallPrefab;

    private float initZ;

    void Start()
    {
        selectedBallPrefab = magneticBallPrefab; // Default selected ball

        if (transform.childCount == 0)
        {
            Instantiate(selectedBallPrefab, transform);
        }

        initZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Select ball type
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedBallPrefab = magneticBallPrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedBallPrefab = heavyBallPrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedBallPrefab = plasticBallPrefab;
        }

        // Generate ball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float zMultiply = 1;
            foreach (Transform child in transform)
            {
                zMultiply *= (child.transform.position.z - initZ);
            }
            if (zMultiply > 0)
            {
                Instantiate(selectedBallPrefab, transform.position, Quaternion.identity, transform);
            }
        }
    }
}
