using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button newGameButton;
    public Button quitGameButton;
    public Button mainMenuButton;

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
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(MainMenu);
        }
        else
        {
            Debug.LogError("Main Menu Button is not assigned in the inspector.");
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
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
