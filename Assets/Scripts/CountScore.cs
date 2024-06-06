using UnityEngine;
using UnityEngine.UI;

public class CountScore : MonoBehaviour
{
    Text showScore;
    void Start() {
        showScore = GetComponent<Text>();
    }
    void Update() {
        if (GameManager.Instance != null)
        {
            showScore.text = "Score:" + GameManager.Instance.score;
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
        
    }
}
