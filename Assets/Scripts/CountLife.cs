using UnityEngine;
using UnityEngine.UI;

public class CountL : MonoBehaviour
{
    Text showLife;
    void Start() {
     showLife = GetComponent<Text>();
    }
    void Update() {
        if (GameManager.Instance != null)
        {
         showLife.text = "Live:" + GameManager.Instance.lives;
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
        
    }
}
