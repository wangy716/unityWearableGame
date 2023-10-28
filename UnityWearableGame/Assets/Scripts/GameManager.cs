using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;

    private int score;
    private int lastScore;

    private ReplayRecorder replayRecorder;

    // Declare the OnGameEnd and OnRoundEnd events
    public delegate void GameEndHandler(int scoreChange);
    public event GameEndHandler OnGameEnd;

    public delegate void RoundEndHandler();
    public event RoundEndHandler OnRoundEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        replayRecorder = FindObjectOfType<ReplayRecorder>();
    }

    public void AddScore(int points)
    {
        lastScore = score;
        score += points;
        UpdateScoreText();

        // Check for highlight replay
        if (score - lastScore > 30)
        {
            // Trigger highlight replay
            EndGame(score - lastScore);
        }
        else
        {
            // Trigger normal replay
            EndRound();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Call this method to trigger the OnGameEnd event
    public void EndGame(int scoreChange)
    {
        if (OnGameEnd != null)
        {
            OnGameEnd(scoreChange);
        }
    }

    // Call this method to trigger the OnRoundEnd event
    public void EndRound()
    {
        if (OnRoundEnd != null)
        {
            OnRoundEnd();
        }
    }

    // Call this method to reset the game state
    public void ResetGameState()
    {
 
        // Add any other necessary code to reset the game state here.
    }
}
