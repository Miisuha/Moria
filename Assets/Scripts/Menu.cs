using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button newGameButton;
    public Button quitGameButton;

    private void Start()
    {
        if (newGameButton != null)
        {
            newGameButton.onClick.AddListener(StartNewGame);
        }
        else
        {
            Debug.LogError("New Game Button is not assigned in the inspector.");
        }

        if (quitGameButton != null)
        {
            quitGameButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("Quit Game Button is not assigned in the inspector.");
        }
    }
    public void StartNewGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.NewGame();
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
