using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int points = 5;
    public int score = 0;
    private bool isSelectingBall = true;
    public bool isSelectingItem = false;

    [SerializeField] private float startZ = -5.84f;

    [Header("Ball Prefabs")]
    [SerializeField] private GameObject magneticBallPrefab;
    [SerializeField] private GameObject heavyBallPrefab;
    [SerializeField] private GameObject plasticBallPrefab;

    [Header("Camera")]
    public CameraFollow cameraFollow;

    [Header("State")]
    public StateSwitch currentState;

    private GameObject selectedBallPrefab;
    private GameObject selectedBallInstance; 
    public bool isShooting = false;

    [Header("Item Prefabs")]
    [SerializeField] private GameObject BananaPrefab;
    [SerializeField] private GameObject GumPrefab;
    [SerializeField] private GameObject HolePrefab;

    [Header("Player Change")]
    public PlayerChange playerChange;

    private GameObject selectedItemPrefab;
    private GameObject selectedItemInstance;


    private float keyPressedTime;
    private float keyReleasedTime;


    [Header("Sound Effects")]
    public AudioSource shootingSound;


    private void Update()
    {
        if (isSelectingBall)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectBall(magneticBallPrefab);
                keyPressedTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectBall(heavyBallPrefab);
                keyPressedTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectBall(plasticBallPrefab);
                keyPressedTime = Time.time;
            }

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if(keyInterval > 2f)
                {
                    points--;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedBallInstance;
                    shootingSound.Play();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > 2f)
                {
                    points--;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedBallInstance;
                    shootingSound.Play();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if(keyInterval > 2f)
                {
                    points--;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedBallInstance;
                    shootingSound.Play();
                }
            }
        } else
        {
            if (!isShooting && !isSelectingItem)
            {
                currentState.toItem = true;
                isSelectingItem = true;
            }
        }

        if (isSelectingItem)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                isShooting = false;
                SelectItem(BananaPrefab);
                keyPressedTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                isShooting = false;
                SelectItem(GumPrefab);
                keyPressedTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                isShooting = false;
                SelectItem(HolePrefab);
                keyPressedTime = Time.time;
            }

            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > 2f)
                {
                    points--;
                    isSelectingItem = false;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedItemInstance;
                    shootingSound.Play();

                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha5))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > 2f)
                {
                    points--;
                    isSelectingItem = false;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedItemInstance;
                    shootingSound.Play();

                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha6))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > 2f)
                {
                    points--;
                    isSelectingItem = false;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedItemInstance;
                    shootingSound.Play();

                }
            }
        }
    }


    public void SelectBall(GameObject ballPrefab)
    {
        if (selectedBallInstance != null)
        {
            Destroy(selectedBallInstance); 
        }

        selectedBallPrefab = ballPrefab;
        selectedBallInstance = Instantiate(selectedBallPrefab);
        selectedBallInstance.transform.position = new Vector3(0, 0, startZ);
        
    }

 

    public void SelectItem(GameObject itemePrefab)
    {
        if (points > 0)
        {
            if (selectedItemInstance != null)
            {
                Destroy(selectedItemInstance);
            }

            selectedItemPrefab = itemePrefab;
            selectedItemInstance = Instantiate(selectedItemPrefab);
            selectedItemInstance.transform.position = new Vector3(0, 0, startZ);
        } else
        {
            if (!isSelectingItem)
            {
                playerChange.EndRound();
            }
        }
    }
}