using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewBall : MonoBehaviour
{

    [SerializeField] StateSwitch stateSwitch;

    [Header("Ball Prefab")]
    [SerializeField] private GameObject[] ballPrefab;

    [Header("Item Prefab")]
    [SerializeField] private GameObject[] itemPrefab;
    
    [Header("Ball Prefabs")]
    [SerializeField] private GameObject magneticBallPrefab;
    [SerializeField] private GameObject heavyBallPrefab;
    [SerializeField] private GameObject plasticBallPrefab;

    private GameObject selectedBallPrefab;
    private float initZ;
    private bool currentPlayerA = true;



    void Start()
    {
        selectedBallPrefab = magneticBallPrefab; // Default selected ball

        if (transform.childCount == 0)
        {
            //Instantiate(ballPrefab[0], transform);
            Instantiate(selectedBallPrefab, transform);
        }

        initZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
//player change
//         if(stateSwitch.currentBallState)
//         {
//             Transform lastChild;
//             lastChild = transform.GetChild(transform.childCount - 1);
//             if (lastChild.transform.position.z > 0)
//             {
//                 if (lastChild.CompareTag("Ball"))
//                 {

//                 }
//             }

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
     

                stateSwitch.toItem = true;
                Instantiate(selectedBallPrefab, transform.position, Quaternion.identity, transform);
            }

        }

        if (!stateSwitch.currentBallState)
        {
            //int extraItem = 1;
            //foreach (Transform child in transform)
            //{
            //    if (child.CompareTag("Banana"))
            //    {
            //        extraItem = 0;
            //    }
            //}
            //if (extraItem == 1)
            //{
            //    Instantiate(itemPrefab[0], transform);
            //}

            if (Input.GetKeyDown("b"))
            {
                float zMultiply = 1;
                foreach (Transform child in transform)
                {
                    zMultiply *= (child.transform.position.z - initZ);
                }
                if (zMultiply > 0)
                {
                    Instantiate(itemPrefab[0], transform);
                }
            }

            if (Input.GetKeyDown("g"))
            {
                float zMultiply = 1;
                foreach (Transform child in transform)
                {
                    zMultiply *= (child.transform.position.z - initZ);
                }
                if (zMultiply > 0)
                {
                    Instantiate(itemPrefab[1], transform);
                }
            }

            if (Input.GetKeyDown("t"))
            {
                float zMultiply = 1;
                foreach (Transform child in transform)
                {
                    zMultiply *= (child.transform.position.z - initZ);
                }
                if (zMultiply > 0)
                {
                    Instantiate(itemPrefab[2], transform);
                }
            }
        
        }
    }
}
