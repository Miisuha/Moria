using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int score { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    public AudioSource src;
    public AudioClip life,coin, over;
    
    
    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        //NewGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;
        score = 0;

        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        // TODO: show game over screen
        src.clip = over;
        src.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        CancelInvoke(nameof(ResetLevel));
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    public void AddCoin()    
    {
        src.clip = coin;
        src.Play();
        coins++;
        score+=200;

        if (coins == 20)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        src.clip = life;
        src.Play();
        lives++;
    }

    public void scoreKill()    
    {
        score+=100;
    }
    
}