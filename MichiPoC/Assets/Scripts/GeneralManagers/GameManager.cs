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
    private bool _gameIsWonOrLost = false;

    public void Update()
    {
        if(_gameIsWonOrLost)
        {
            bool anyRelevantKeyDown = false;

            if(Input.GetButtonDown("RestartLevel"))
            {
                anyRelevantKeyDown = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if(Input.GetButtonDown("GoToMenu"))
            {
                anyRelevantKeyDown = true;
                SceneManager.LoadScene("Menu");
            }

            if(anyRelevantKeyDown)
            {
                _currentScore = 0;
                Time.timeScale = 1.0f;
                _gameIsWonOrLost = false;
            }
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
        if (_gameIsWonOrLost) return;

        Time.timeScale = 0.0f;
        EventSystem.Instance.ShowGameOverScreen(reason);

        _gameIsWonOrLost = true;
    }

    private void WonGame()
    {
        if (_gameIsWonOrLost) return;

        Time.timeScale = 0.0f;
        EventSystem.Instance.ShowGameWonScreen(_currentScore);

        _gameIsWonOrLost = true;

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
