using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerChange : MonoBehaviour
{
    public Player playerA;
    public Player playerB;
    public int totalRounds = 5;
    private int currentRound = 1;
    public Player currentPlayer;

    [Header("Points")]
    [SerializeField] TMP_Text playerAPoint;
    [SerializeField] TMP_Text playerBPoint;

    private void Start()
    {
        currentPlayer = playerA; // Player A starts
        playerA.enabled = true;
        playerB.enabled = false;
        // Initialize your player objects, their scores, and any other game-related data.
        // Start the first round
        StartRound();
    }

    private void Update()
    {
        // Update points
        playerAPoint.text = (playerA.points).ToString();
        playerBPoint.text = (playerB.points).ToString();
    }

    private void StartRound()
    {
        //currentPlayer.EndTurn();
        // Handle player ball selection here
        // Update UI to indicate ball selection phase
    }

   

    public void EndRound()
    {
        //currentPlayer.EndTurn();
        if (currentRound >= totalRounds)
        {
            // Game over logic
        }
        else
        {
            // Start a new round
            currentRound++;
            currentPlayer = (currentPlayer == playerA) ? playerB : playerA;
            StartRound();
        }
    }
}
