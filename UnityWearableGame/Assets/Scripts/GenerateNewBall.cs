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
    private ReplayRecorder replayRecorder;

    private float initZ;

    void Start()
    {

        selectedBallPrefab = magneticBallPrefab; // Default selected ball

        if (transform.childCount == 0)
        {
            Instantiate(selectedBallPrefab, transform);
        }
        replayRecorder = FindObjectOfType<ReplayRecorder>();

        initZ = transform.position.z;
    }


    //private void InstantiateBall()
    //{
    //    GameObject newBall = Instantiate(selectedBallPrefab, transform.position, Quaternion.identity, transform);
    //    replayRecorder.AddGameObjectToRecord(newBall);  // Add the new ball to the list of game objects to record
    //}


    private void InstantiateBall()
    {
        Debug.Log("Trying to instantiate a new ball...");
        if (!replayRecorder.IsReplaying)  // Check if a replay is in progress
        {
            Debug.Log("Instantiating a new ball...");
            GameObject newBall = Instantiate(selectedBallPrefab, transform.position, Quaternion.identity, transform);
            replayRecorder.AddGameObjectToRecord(newBall);  // Add the new ball to the list of game objects to record

            // Debug logs to check ball's position and scale
            Debug.Log("New ball position: " + newBall.transform.position);
            Debug.Log("New ball scale: " + newBall.transform.localScale);
        }
        else
        {
            Debug.Log("Cannot instantiate a new ball because a replay is in progress.");
        }
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
                InstantiateBall();  // Call the InstantiateBall method to instantiate and record the new ball
            }
        }
    }
}
