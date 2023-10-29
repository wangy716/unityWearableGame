using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayRecorder : MonoBehaviour
{
    [Header("Game Objects to Record")]
    [SerializeField] private List<GameObject> gameObjectsToRecord = new List<GameObject>();

    private List<FrameData> recordedFrames;
    private bool isRecording;
    public bool IsReplaying { get; private set; }  // Add this property

    private void Start()
    {
        recordedFrames = new List<FrameData>();
        isRecording = true;
    }

    private void Update()
    {

        if (isRecording)
        {
            RecordFrame();
        }
    }

    public void StartRecording()
    {
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void AddGameObjectToRecord(GameObject gameObject)
    {
        if (!gameObjectsToRecord.Contains(gameObject))
        {
            gameObjectsToRecord.Add(gameObject);
        }
    }

    public void ClearRecordedFrames()
    {
        recordedFrames.Clear();
    }

    private void RecordFrame()
    {
        FrameData frameData = new FrameData();
        foreach (GameObject gameObject in gameObjectsToRecord)
        {
            if (gameObject != null)
            {
                ObjectData objectData = new ObjectData();
                objectData.position = gameObject.transform.position;
                objectData.rotation = gameObject.transform.rotation;
                frameData.objectDataList.Add(objectData);
            }
        }
        recordedFrames.Add(frameData);
    }

    public List<FrameData> GetRecordedFrames()
    {
        return recordedFrames;
    }

    [System.Serializable]
    public class FrameData
    {
        public List<ObjectData> objectDataList = new List<ObjectData>();
    }

    [System.Serializable]
    public class ObjectData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    public void ClearGameObjectsToRecord()
    {
        gameObjectsToRecord.Clear();
    }
}
