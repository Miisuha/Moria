using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int score { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
    private HashSet<string> destroyedObjects;

    public AudioSource src;
    public AudioClip life, coin, over;

    private Vector3 initialPlayerPosition;
    private GameObject player;
    private ScoreManager scoreManager;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            destroyedObjects = new HashSet<string>();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        // NewGame();
        SceneManager.LoadScene("MainMenu");

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            GameObject scoreManagerObject = new GameObject("ScoreManager");
            scoreManager = scoreManagerObject.AddComponent<ScoreManager>();
        }
    }

    public void RegisterPlayer(GameObject player)
    {
        this.player = player;
        initialPlayerPosition = player.transform.position;
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;
        score = 0;
        destroyedObjects.Clear();

        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        // Save the score
        scoreManager.SaveScore(score);

        // TODO: show game over screen
        src.clip = over;
        src.Play();
        SceneManager.LoadScene("GameOver");
    }

    public void Win()
    {
        // Save the score
        scoreManager.SaveScore(score);

        // TODO: show game over screen
        SceneManager.LoadScene("Win");
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

        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.transform.position = initialPlayerPosition;
            // Optionally reset player's velocity and other states here if needed
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void AddCoin()
    {
        src.clip = coin;
        src.Play();
        coins++;
        score += 200;

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
        score += 100;
    }

    public void RegisterDestroyedObject(string objectId)
    {
        if (!destroyedObjects.Contains(objectId))
        {
            destroyedObjects.Add(objectId);
        }
    }

    public bool IsObjectDestroyed(string objectId)
    {
        return destroyedObjects.Contains(objectId);
    }

    public List<int> GetTopScores()
    {
        return scoreManager.GetTopScores();
    }
}
