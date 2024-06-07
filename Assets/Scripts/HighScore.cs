using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HighScore : MonoBehaviour
{
    Text showScore;

    void Start()
    {
        showScore = GetComponent<Text>();
        UpdateHighScoreText();
    }

    void Update()
    {

    }

    void UpdateHighScoreText()
    {
        if (GameManager.Instance != null)
        {
            List<int> topScores = GameManager.Instance.GetTopScores();
            showScore.text = "High Scores:\n";

            for (int i = 0; i < topScores.Count; i++)
            {
                showScore.text += (i + 1) + ": " + topScores[i] + "\n";
            }
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }
}
