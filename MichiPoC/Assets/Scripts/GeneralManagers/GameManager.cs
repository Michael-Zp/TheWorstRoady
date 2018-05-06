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

    public bool[] UnlockedLevels = { true, false, false, false };
    
    private bool _gameIsWon = false;
    private bool _gameIsLost = false;
    
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
        EventSystem.Instance.UnlockLevelEvent += UnlockLevel;
    }

    public void Update()
    {
        bool anyRelevantKeyDown = false;

        if (_gameIsWon || _gameIsLost)
        {
            if (Input.GetButtonDown("RestartLevel"))
            {
                Debug.Log("Restart");
                anyRelevantKeyDown = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetButtonDown("GoToMenu"))
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
            Time.timeScale = 1.0f;
            _gameIsWon = false;
            _gameIsLost = false;
        }
    }

    private void OnDestroy()
    {
        EventSystem.Instance.GameWonEvent -= WonGame;
        EventSystem.Instance.GameOverEvent -= GameOver;
        EventSystem.Instance.UnlockLevelEvent -= UnlockLevel;
    }

    private void GameOver()
    {
        if (_gameIsLost || _gameIsWon) return;

        Time.timeScale = 0.0f;
        EventSystem.Instance.ShowGameOverScreen();

        _gameIsLost = true;
    }

    private void WonGame()
    {
        if (_gameIsLost || _gameIsWon) return;

        Time.timeScale = 0.0f;
        EventSystem.Instance.ShowGameWonScreen();

        _gameIsWon = true;
    }

    private void UnlockLevel(int level)
    {
        if(level - 1 < UnlockedLevels.Length)
        {
            UnlockedLevels[level] = true;
        }
    }
}
