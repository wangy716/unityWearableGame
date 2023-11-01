using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public TextMeshProUGUI scoreTextA;
    public TextMeshProUGUI scoreTextB;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetScores(int scoreA, int scoreB)
    {
        if (scoreTextA != null)
        {
            scoreTextA.text = "Player A Score: " + scoreA;
        }
        if (scoreTextB != null)
        {
            scoreTextB.text = "Player B Score: " + scoreB;
        }
    }
}
