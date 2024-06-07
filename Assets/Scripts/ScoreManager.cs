using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    private string filePath;
    private List<int> topScores;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "highscores.txt");
        LoadScores();
    }

    public void SaveScore(int score)
    {
        topScores.Add(score);
        topScores = topScores.OrderByDescending(s => s).Take(5).ToList();
        SaveScores();
    }

    private void LoadScores()
    {
        topScores = new List<int>();

        if (File.Exists(filePath))
        {
            string[] scoreStrings = File.ReadAllLines(filePath);
            foreach (string scoreString in scoreStrings)
            {
                if (int.TryParse(scoreString, out int score))
                {
                    topScores.Add(score);
                }
            }
            topScores = topScores.OrderByDescending(s => s).Take(5).ToList();
        }
    }

    private void SaveScores()
    {
        File.WriteAllLines(filePath, topScores.Select(score => score.ToString()).ToArray());
    }

    public List<int> GetTopScores()
    {
        return new List<int>(topScores);
    }
}
