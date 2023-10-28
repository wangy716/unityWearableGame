using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;

    private void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.01f)
        {
            CheckScoringArea();
        }
    }

    private void CheckScoringArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        int newScore = 0;

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "ScoringArea1")
            {
                newScore = 10;
                break;
            }
            else if (collider.tag == "ScoringArea2")
            {
                newScore = 20;
                break;
            }
            else if (collider.tag == "ScoringArea3")
            {
                newScore = 30;
                break;
            }
        }

        if (newScore != currentScore)
        {
            GameManager.Instance.AddScore(newScore - currentScore);
            currentScore = newScore;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Board")
        {
            GameManager.Instance.AddScore(-currentScore);
            currentScore = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trap")
        {
            GameManager.Instance.AddScore(-currentScore);
            currentScore = 0;
            Destroy(gameObject);  // Destroy the ball if it falls into a trap
        }
    }
}
