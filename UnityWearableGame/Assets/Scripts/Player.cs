using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int points = 0;
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
    //public StateSwitch currentState;
    public TMP_Text currentState;
    public PlayerChange playerChange;

    private GameObject selectedBallPrefab;
    //private GameObject selectedBallInstance; 
    public bool isShooting = false;

    [Header("Item Prefabs")]
    [SerializeField] private GameObject BananaPrefab;
    [SerializeField] private GameObject GumPrefab;
    [SerializeField] private GameObject HolePrefab;

    [Header("Arduino")]
    [SerializeField] private ArduinoConnect ArduinoConnect;
    public AudioSource audioSource; 



    private bool plasticGes;
    private bool heavyGes;
    private bool magGes;
    private bool bananaGes;
    private bool gumGes;
    private bool trapGes;

    private float holdTimePlastic;
    private float holdTimeHeavy;
    private float holdTimeMag;
    private float lockGes = 1f;

    private GameObject selectedItemPrefab;


    private float keyPressedTime;
    private float keyReleasedTime;
    private float selectionTime = 1f;

    private bool plasticSelected;

    private void Update()
    {
        if (isSelectingBall)
        {
            currentState.text = "Choose Your Ball";

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isShooting = false;
                selectedBallPrefab = magneticBallPrefab;
                //SelectBall(magneticBallPrefab);
                keyPressedTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                isShooting = false;
                selectedBallPrefab = heavyBallPrefab;
                //SelectBall(heavyBallPrefab);
                keyPressedTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                isShooting = false;
                selectedBallPrefab = plasticBallPrefab;
                //SelectBall(plasticBallPrefab);
                keyPressedTime = Time.time;
            }

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > selectionTime)
                {
                    audioSource.Play();
                    GameObject selectedBallInstance = Instantiate(selectedBallPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                    points--;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedBallInstance;


                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > selectionTime)
                {
                    audioSource.Play();
                    GameObject selectedBallInstance = Instantiate(selectedBallPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                    points--;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedBallInstance;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > selectionTime)
                {
                    audioSource.Play();
                    GameObject selectedBallInstance = Instantiate(selectedBallPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                    points--;
                    isSelectingBall = false;
                    isShooting = true;
                    cameraFollow.selectedOne = selectedBallInstance;
                }
            }

            BallCheckGesture();

            if (plasticGes)
            {
                isShooting = false;
                selectedBallPrefab = plasticBallPrefab;
                GameObject selectedBallInstance = Instantiate(selectedBallPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                points--;
                isSelectingBall = false;
                isShooting = true;
                cameraFollow.selectedOne = selectedBallInstance;

            }

            if (heavyGes)
            {
                isShooting = false;
                selectedBallPrefab = heavyBallPrefab;
                GameObject selectedBallInstance = Instantiate(selectedBallPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                points--;
                isSelectingBall = false;
                isShooting = true;
                cameraFollow.selectedOne = selectedBallInstance;
            }

            if (magGes)
            {
                isShooting = false;
                selectedBallPrefab = magneticBallPrefab;
                GameObject selectedBallInstance = Instantiate(selectedBallPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                points--;
                isSelectingBall = false;
                isShooting = true;
                cameraFollow.selectedOne = selectedBallInstance;
            }

        }
        else
        {
            if (!isShooting && !isSelectingItem)
            {
                //currentState.toItem = true;
                //currentState.toBall = false;
                heavyGes = false;
                plasticGes = false;
                magGes = false;
                isSelectingItem = true;
            }
        }

        if (isSelectingItem)
        {
            currentState.text = "Choose Your Item";

            if (Input.GetKeyDown("space"))
            {
                playerChange.EndRound();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                //isShooting = false;

                keyPressedTime = Time.time;

                selectedItemPrefab = BananaPrefab;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && points > 2)
            {
                //isShooting = false;
                //SelectItem(GumPrefab);

                keyPressedTime = Time.time;

                selectedItemPrefab = GumPrefab;

            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) && points > 6)
            {
                //isShooting = false;
                //SelectItem(HolePrefab);

                keyPressedTime = Time.time;

                selectedItemPrefab = HolePrefab;
            }

            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > selectionTime)
                {
                    audioSource.Play();
                    Instantiate(selectedItemPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                    points--;
                    isSelectingItem = false;
                    isShooting = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha5) && points > 2)
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > selectionTime)
                {
                    audioSource.Play();
                    Instantiate(selectedItemPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                    points -= 3;
                    isSelectingItem = false;
                    isShooting = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha6) && points > 6)
            {
                keyReleasedTime = Time.time;
                float keyInterval = keyReleasedTime - keyPressedTime;
                if (keyInterval > selectionTime)
                {
                    audioSource.Play();
                    Instantiate(selectedItemPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                    points -= 7;
                    isSelectingItem = false;
                    isShooting = true;
                }
            }

            ItemCheckGesture();

            if (bananaGes)
            {
                selectedItemPrefab = BananaPrefab;
                Instantiate(selectedItemPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                points--;
                isSelectingItem = false;
                isShooting = true;
            }

            if (gumGes && points > 2)
            {
                selectedItemPrefab = GumPrefab;
                Instantiate(selectedItemPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                points-=3;
                isSelectingItem = false;
                isShooting = true;
            }

            if (trapGes && points > 6)
            {
                selectedItemPrefab = HolePrefab;
                Instantiate(selectedItemPrefab, new Vector3(0, 0, startZ), Quaternion.identity);
                points-=7;
                isSelectingItem = false;
                isShooting = true;
            }
        }
        else
        {
            bananaGes = false;
            gumGes = false;
            trapGes = false;
        }
    }

    //private void SelectBall(GameObject ballPrefab)
    //{
    //    if (selectedBallInstance != null)
    //    {
    //        Destroy(selectedBallInstance); 
    //    }

    //    selectedBallPrefab = ballPrefab;
    //    selectedBallInstance = Instantiate(selectedBallPrefab);
    //    selectedBallInstance.transform.position = new Vector3(0, 0, startZ);

    //}

    private void BallCheckGesture()
    {
        // Plastic Ball
        if (ArduinoConnect.finger1 < 600 && ArduinoConnect.finger2 > 800 && ArduinoConnect.finger3 > 800 && ArduinoConnect.finger4 > 800 && ArduinoConnect.finger5 > 800)
        {
            holdTimePlastic += Time.deltaTime;
            if (holdTimePlastic >= lockGes)
            {
                plasticGes = true;
            }
        }
        else
        {
            holdTimePlastic = 0;
        }

        // Heavy Ball
        if (ArduinoConnect.finger1 < 600 && ArduinoConnect.finger2 < 600 && ArduinoConnect.finger3 > 800 && ArduinoConnect.finger4 > 800 && ArduinoConnect.finger5 > 800)
        {
            holdTimeHeavy += Time.deltaTime;
            if (holdTimeHeavy >= lockGes)
            {
                heavyGes = true;
            }
        }
        else
        {
            holdTimeHeavy = 0;
        }

        // Mag Ball
        if (ArduinoConnect.finger1 < 600 && ArduinoConnect.finger2 < 600 && ArduinoConnect.finger3 > 800 && ArduinoConnect.finger4 > 800 && ArduinoConnect.finger5 < 600)
        {
            holdTimeMag += Time.deltaTime;
            if (holdTimeMag >= lockGes)
            {
                magGes = true;
            }
        }
        else
        {
            holdTimeMag = 0;
        }
    }

    private void ItemCheckGesture()
    {
        // Banana
        if (ArduinoConnect.finger1 > 800 && ArduinoConnect.finger2 > 800 && ArduinoConnect.finger3 < 600 && ArduinoConnect.finger4 < 600 && ArduinoConnect.finger5 < 600)
        {
            holdTimePlastic += Time.deltaTime;
            if (holdTimePlastic >= lockGes)
            {
                bananaGes = true;
            }
        }
        else
        {
            holdTimePlastic = 0;
        }

        // Gum
        if (ArduinoConnect.finger1 > 800 && ArduinoConnect.finger2 > 800 && ArduinoConnect.finger3 > 800 && ArduinoConnect.finger4 > 800 && ArduinoConnect.finger5 < 600)
        {
            holdTimeHeavy += Time.deltaTime;
            if (holdTimeHeavy >= lockGes)
            {
                gumGes = true;
            }
        }
        else
        {
            holdTimeHeavy = 0;
        }

        // Trap
        if (ArduinoConnect.finger1 > 800 && ArduinoConnect.finger2 > 800 && ArduinoConnect.finger3 < 600 && ArduinoConnect.finger4 > 800 && ArduinoConnect.finger5 > 800)
        {
            holdTimeMag += Time.deltaTime;
            if (holdTimeMag >= lockGes)
            {
                trapGes = true;
            }
        }
        else
        {
            holdTimeMag = 0;
        }
    }

    public void ResetPlayer()
    {
        isShooting = false;
        isSelectingBall = true;
        isSelectingItem = false;
    }


}
