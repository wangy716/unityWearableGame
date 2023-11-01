using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreTextA;
    public TextMeshProUGUI scoreTextB;

    [SerializeField] PlayerChange playerChange;

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
        lastScore = playerChange.currentPlayer.score;
        playerChange.currentPlayer.score += points;
        UpdateScoreText();

        // Check for highlight replay
        if (playerChange.currentPlayer.score - lastScore > 30)
        {
            // Trigger highlight replay
            EndGame(playerChange.currentPlayer.score - lastScore);
        }
        else
        {
            // Trigger normal replay
            EndRound();
        }
    }

    private void UpdateScoreText()
    {
        if (playerChange.currentPlayer == playerChange.playerA)
        {
            if (scoreTextA != null)
            {
                scoreTextA.text = "Score: " + playerChange.currentPlayer.score;
            }
        }

        if (playerChange.currentPlayer == playerChange.playerB)
        {
            if (scoreTextB != null)
            {
                scoreTextB.text = "Score: " + playerChange.currentPlayer.score;
            }
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


    public void GameOver()
    {
        SceneManager.LoadScene("03End");
        //if (GameOverManager.Instance != null)
        //{
        //    if (playerChange != null)
        //    {
        //        GameOverManager.Instance.SetScores(playerChange.playerA.score, playerChange.playerB.score);
        //    }
        //    else
        //    {
        //        Debug.LogError("PlayerChange instance is null");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("GameOverManager instance is null");
        //}
    }

}
