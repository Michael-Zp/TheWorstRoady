using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    public bool[] UnlockedLevels = { true, false, false };

    private int _currentScore;
    private bool _gameIsWon = false;
    private bool _gameIsLost = false;

    public void Update()
    {
        bool anyRelevantKeyDown = false;

        if (_gameIsWon || _gameIsLost)
        {
            if(Input.GetButtonDown("RestartLevel"))
            {
                Debug.Log("Restart");
                anyRelevantKeyDown = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if(Input.GetButtonDown("GoToMenu"))
            {
                Debug.Log("Menu");
                anyRelevantKeyDown = true;
                SceneManager.LoadScene("Menu");
            }
        }

        if (_gameIsWon)
        {
            if (Input.GetButtonDown("GoToNextLevel"))
            {
                Debug.Log("Next");
                anyRelevantKeyDown = true;
                EventSystem.Instance.LoadNextLevel();
            }
        }
        
        if (anyRelevantKeyDown)
        {
            _currentScore = 0;
            Time.timeScale = 1.0f;
            _gameIsWon = false;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }

        EventSystem.Instance.GameWonEvent += WonGame;
        EventSystem.Instance.GameOverEvent += GameOver;
        EventSystem.Instance.AddScoreEvent += AddScore;
        EventSystem.Instance.UnlockLevelEvent += UnlockLevel;
    }

    private void OnDestroy()
    {
        EventSystem.Instance.GameWonEvent -= WonGame;
        EventSystem.Instance.GameOverEvent -= GameOver;
        EventSystem.Instance.AddScoreEvent -= AddScore;
        EventSystem.Instance.UnlockLevelEvent -= UnlockLevel;
    }


    private void AddScore(int score)
    {
        _currentScore += score;
        EventSystem.Instance.ShowScore(_currentScore);
    }

    private void GameOver(string reason)
    {
        if (_gameIsLost) return;

        Time.timeScale = 0.0f;
        EventSystem.Instance.ShowGameOverScreen(reason);

        _gameIsLost = true;
    }

    private void WonGame()
    {
        if (_gameIsWon) return;

        Time.timeScale = 0.0f;
        EventSystem.Instance.ShowGameWonScreen(_currentScore);

        _gameIsWon = true;

        HighscoresData.AddHighscore(_currentScore);
    }

    private void UnlockLevel(int level)
    {
        if(level - 1 < UnlockedLevels.Length)
        {
            UnlockedLevels[level] = true;
        }
    }
}
