using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateSwitch : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TMP_Text currentState;

    [Header("Background Color")]
    [SerializeField] private Color ballColor;
    [SerializeField] private Color itemColor;
    [SerializeField] private float colorChangeSpeed;

    [Header("Current State")]
    public bool currentBallState;
    public bool toItem;
    public bool toBall;

    [Header("Player Switch")]
    public int usablePointA = 5;
    public int usablePointB = 5;
    public bool nowPlayerA;
    public bool nowPlayerB;
    public bool playerChangeNow;
    [SerializeField] TMP_Text currentPlayer;

    private float changeTime = 0;
    

    void Start()
    {
        //ballColor = new Color(95, 136, 202);
        //itemColor = new Color(219, 167, 56);
        //Camera.main.backgroundColor = ballColor;
        currentBallState = true;
        nowPlayerA = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GameStateChange());

        CheckPlayerChange();

        StartCoroutine(PlayerChange());

        
    }

    private IEnumerator GameStateChange()
    {
        // Choose Ball
        if (toBall)
        {
            currentState.text = "Choose Your Ball";
            StartCoroutine(ColorChange(itemColor, ballColor));
            currentBallState = true;
        }

        // Choose Item
        if (toItem)
        {
            currentState.text = "Choose Your Item";
            StartCoroutine(ColorChange(ballColor, itemColor));
            toItem = false;
            currentBallState = false;
        }

        yield return null;
    }

    private IEnumerator ColorChange(Color colorPrevious, Color colorNow)
    {
        Camera.main.backgroundColor = Color.Lerp(colorPrevious, colorNow, changeTime);
        changeTime += Time.deltaTime;

        if (changeTime > 1)
        {
            changeTime = 0;
            toItem = false;
            toBall = false;
            yield return null;
        }
        
    }

    private IEnumerator PlayerChange()
    {
        // Change Player
        if (playerChangeNow)
        {
            playerChangeNow = false;
            toBall = true;

            if (nowPlayerA)
            {
                currentPlayer.text = "Player A";
            }

            if (nowPlayerB)
            {
                currentPlayer.text = "Player B";
            }
        }
        yield return null;
    }

    private void CheckPlayerChange()
    {
        if (nowPlayerA)
        {
            if(!currentBallState && (usablePointA == 0 || Input.GetKeyDown("c")))
            {
                nowPlayerA = false;
                nowPlayerB = true;
                playerChangeNow = true;
            }
        }

        if (nowPlayerB)
        {
            if (!currentBallState && (usablePointB == 0 || Input.GetKeyDown("c")))
            {
                nowPlayerA = true;
                nowPlayerB = false;
                playerChangeNow = true;
            }
        }
    }
}
