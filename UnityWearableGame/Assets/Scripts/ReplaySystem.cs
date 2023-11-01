using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour
{
    [Header("Replay Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera orthographicCamera;
    [SerializeField] private Camera ballCamera;

    [Header("Game Objects to Replay")]
    [SerializeField] private List<GameObject> gameObjectsToReplay = new List<GameObject>();

    [Header("Orthographic Camera Settings")]
    [SerializeField] private float orthographicCameraSpeed = 1f; // Adjust this value to change the speed of the camera movement
    [SerializeField] private Vector3 orthographicCameraStartPosition;

    [Header("Player")]
    public PlayerChange playerChange;

    private ReplayRecorder replayRecorder;
    private bool isReplaying;

    private void Start()
    {
        mainCamera.gameObject.SetActive(true);
        orthographicCamera.gameObject.SetActive(false);
        ballCamera.gameObject.SetActive(false);
        replayRecorder = FindObjectOfType<ReplayRecorder>();
        isReplaying = false;

        // Subscribe to the event that triggers the replay
        GameManager.Instance.OnGameEnd += StartReplay;
        GameManager.Instance.OnRoundEnd += StartNormalReplay; // Subscribe to the normal replay event
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameEnd -= StartReplay;
            GameManager.Instance.OnRoundEnd -= StartNormalReplay; // Unsubscribe from the normal replay event
        }
    }

    public void StartReplay(int scoreChange)
    {
        if (isReplaying) return; // Prevent starting a new replay if one is already in progress

        Debug.Log("Starting Replay with score change: " + scoreChange);
        if (replayRecorder == null)
        {
            Debug.LogError("ReplayRecorder not found!");
            return;
        }

        List<ReplayRecorder.FrameData> recordedFrames = replayRecorder.GetRecordedFrames();
        StartCoroutine(PlayReplay(recordedFrames, scoreChange));
    }

    public void StartNormalReplay() // Function to start normal replay
    {
        if (isReplaying) return; // Prevent starting a new replay if one is already in progress

        Debug.Log("Starting Normal Replay");
        if (replayRecorder == null)
        {
            Debug.LogError("ReplayRecorder not found!");
            return;
        }

        replayRecorder.StopRecording(); // Stop recording at the end of the round
        List<ReplayRecorder.FrameData> recordedFrames = replayRecorder.GetRecordedFrames();
        StartCoroutine(PlayReplay(recordedFrames, 0)); // Pass 0 as scoreChange for normal replay
    }

    private IEnumerator PlayReplay(List<ReplayRecorder.FrameData> recordedFrames, int scoreChange)
    {
        isReplaying = true;

        // Switch to the appropriate camera
        if (scoreChange > 30)
        {
            ballCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            orthographicCamera.gameObject.SetActive(false);

            // Set the position of the ball-following camera to follow the last ball
            GameObject lastBall = gameObjectsToReplay[gameObjectsToReplay.Count - 1];
            ballCamera.transform.position = lastBall.transform.position + new Vector3(0, 5, -5);
            ballCamera.transform.LookAt(lastBall.transform);
        }
        else
        {
            orthographicCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            ballCamera.gameObject.SetActive(false);
        }

        float replayDuration = 3f;  // Set a fixed duration for the replay
        float startTime = Time.time;

        List<GameObject> gameObjectsCopy = new List<GameObject>(gameObjectsToReplay);  // Create a copy of the list
        foreach (ReplayRecorder.FrameData frameData in recordedFrames)
        {
            if (Time.time - startTime > replayDuration) break;  // Break the loop if the replay duration has passed

            for (int i = 0; i < gameObjectsCopy.Count; i++)  // Iterate over the copy of the list
            {
                if (i < frameData.objectDataList.Count)
                {
                    gameObjectsCopy[i].transform.position = frameData.objectDataList[i].position;
                    gameObjectsCopy[i].transform.rotation = frameData.objectDataList[i].rotation;
                }
            }
            yield return null;
        }

        // Switch back to main camera after the fixed duration has passed
        mainCamera.gameObject.SetActive(true);
        orthographicCamera.gameObject.SetActive(false);
        ballCamera.gameObject.SetActive(false);

        isReplaying = false;

        playerChange.currentPlayer.isShooting = false;
    }

}
