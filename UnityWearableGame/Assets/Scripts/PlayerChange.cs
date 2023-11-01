using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerChange : MonoBehaviour
{
    public Player playerA;
    public Player playerB;
    public int totalBigRounds = 2;
    private int currentBigRound = 0;
    private int currentPlayerRound = 0;
    public Player currentPlayer;

    [SerializeField] TMP_Text playerActive;

    [Header("Points")]
    [SerializeField] TMP_Text playerAPoint;
    [SerializeField] TMP_Text playerBPoint;

    private void Start()
    {
        currentPlayer = playerA; // Player A starts
        playerA.enabled = true;
        currentPlayer.points += 5;
        playerActive.text = "Player A";
        playerB.enabled = false;
    }

    private void Update()
    {
        playerAPoint.text = (playerA.points).ToString();
        playerBPoint.text = (playerB.points).ToString();
    }

    public void EndRound()
    {
        Debug.Log("EndRound called");

        Debug.Log("totalBigRounds: " + totalBigRounds);

        if (currentBigRound > totalBigRounds)
        {
            // Game over logic
            Debug.Log("Game over logic triggered");
            GameManager.Instance.GameOver();
        }
        else
        {
            if (currentPlayerRound % 2 == 0)
            {
                currentBigRound++;
            }
            Debug.Log("currentBigRound: " + currentBigRound);
            // Start a new round
            currentPlayerRound++;
            currentPlayer.ResetPlayer();
            currentPlayer.enabled = false;
            Debug.Log("currentPlayerRound: " + currentPlayerRound);
            currentPlayer = (currentPlayer == playerA) ? playerB : playerA;
            currentPlayer.enabled = true;
            currentPlayer.points += 5;

            if (currentPlayer == playerA)
            {
                playerActive.text = "Player A";
            }
            else
            {
                playerActive.text = "Player B";
            }

           
        }
    }
}
